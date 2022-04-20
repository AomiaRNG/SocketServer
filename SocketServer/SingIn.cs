using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketServer
{
    class SingIn
    {
        public string Name;
        public int clientPort;
        public int myPort;
        public IPAddress myIp = IPAddress.Any;
        public IPAddress ipClient;
        public SingIn()
        {
            GetIpClient();
            GetClientPort();
            GetMyPort();
        }
        private void GetIpClient()
        {
            Console.WriteLine("Введите ip собеседника: ");
            ipClient = IPAddress.Parse(Console.ReadLine());
        }
        private void GetClientPort()
        {
            Console.WriteLine("Введите port, по которому хотите подключиться: ");
            clientPort = int.Parse(Console.ReadLine());
        }
        private void GetMyPort()
        {
            Console.WriteLine("Введите port, который желаете открыть для подключения: ");
            myPort = int.Parse(Console.ReadLine());
        }        
    }
}
