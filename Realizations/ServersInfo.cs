using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServersDistribution.Realizations
{
    public class ServersInfo
    {
        private int _numServer;
        public ServersInfo(int weight,double time,int numServer)
        {
            this.ServerWeight = weight;
            this.AverageWorkTime = time;
            _numServer = numServer;
        }
        public int ServerWeight { get; set; }
        public double AverageWorkTime { get; set; }
        public int GetNumServer() { return _numServer; }
    }
}
