using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ServersDistribution.Interfaces;
using ServersDistribution.Realizations;
namespace ServersDistribution
{
    class Program
    {        
        static void Main(string[] args)
        {
            const int NUMSERVERS = 3;
            var logInfo = new LogGetInfo();
            ServersDistribution distribution = new ServersDistribution(NUMSERVERS, logInfo);
            Console.WriteLine("Enter nginx log filePath:");
            string logPath = Console.ReadLine();
            distribution.AnalyzeLogFile(logPath);
            Console.WriteLine("Enter config output filePath:");
            string configPath = Console.ReadLine();
            distribution.WriteConfigInFile(configPath);
        }
    }
}
