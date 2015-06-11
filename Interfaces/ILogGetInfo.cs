using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServersDistribution.Interfaces
{
    public interface ILogGetInfo
    {
        List<double> LogGetTime(string log);//получение времени из лога
        List<string> LogGetServerAddresses(string log);//получение адресов серверов из лога
        int GetServerNumber(string address);//получение номера сервера из его адреса

    }
}
