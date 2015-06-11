using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ServersDistribution.Interfaces;
using ServersDistribution.Realizations;
namespace ServersDistribution
{
    public class ServersDistribution
    {
        private int _numServers;
        private ILogGetInfo _logInfo;
        private List<ServersInfo> _serversInfo;
        public ServersDistribution(int numServers,ILogGetInfo logInfo)
        {
            _numServers = numServers;
            _logInfo = logInfo;
            InitServersInfo();
        }
        private void InitServersInfo()
        {
            _serversInfo = new List<ServersInfo>();
            for(int i = 0; i < _numServers; i++)
            {
                _serversInfo.Add(new ServersInfo(0,0,i+1));//среднее время 0 вес 0 номер i+1
            }
        }
        public void AnalyzeLogFile(string filePath)//анализ файла логов
        {
            using(StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while(!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    AnalyzeLog(line);
                }
            }
        }
        private void AnalyzeLog(string log)//анализ лога
        {
            //основной метод анализа
            List<double> processingTimes = LogGetTime(log);
            List<string> addresses = LogGetServerAddresses(log);
            if(addresses.Count > 1)//ошибка запрос перенаправился
            {

            }
            else 
            {
                if (addresses.Count == 1 && processingTimes.Count == 1)//успешный запрос
                {
                    int serverNumber = GetServerNumber(addresses[0]);
                    AddServerWeight(_serversInfo, serverNumber);
                }
                else return;
            }
        }
        private void AddServerWeight(List<ServersInfo> list, int numServer)//получение информации о сервере
        {
            for (int i = 0; i < list.Count; i++)
            {
                ServersInfo tmp = list[i];
                if (tmp.GetNumServer() == numServer)
                {
                    list[i].ServerWeight++;
                }
            }
        }
        private List<double> LogGetTime(string log)//получение времени из лога
        {
            return _logInfo.LogGetTime(log);
        }
        private List<string> LogGetServerAddresses(string log)//получение адресов серверов из лога
        {
            return _logInfo.LogGetServerAddresses(log);
        }
        private int GetServerNumber(string address)//получение номера сервера из адреса
        {
            return _logInfo.GetServerNumber(address);
        }
        public void WriteConfigInFile(string filePath)//запись конфига в файл
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("upstream nlb_general {");
                for (int i = 0; i < _numServers; i++)
                {
                    ServersInfo tmp = _serversInfo[i];
                    writer.WriteLine("server srv"+tmp.GetNumServer()+".tldomain weight = "+tmp.ServerWeight+";");
                }
                writer.Write("}");
            }
        }
    }
}
