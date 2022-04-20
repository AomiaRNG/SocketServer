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
            GetName();
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
        private void GetName()
        {
            Console.WriteLine("Введите имя: ");
            Name = Console.ReadLine();
        }

        public void GetInfo()
        {
            Console.WriteLine("Ваше имя: " + Name);
            Console.WriteLine("Ваш ip: " + myIp);
            Console.WriteLine("Ваш port: " + myPort);
            Console.WriteLine("Запрос на подключение к: " + ipClient + ":" + clientPort);
        }
    }
}
