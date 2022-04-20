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
    class Program
    {
        static bool isClientOnline = true;

        static List<string> queue = new List<string>();

        static Dictionary<string, string> dictionary = new Dictionary<string, string>();

        static void StartServer()
        {
            SingIn user = new SingIn();            
            
            IPEndPoint myEndPoint = new IPEndPoint(user.myIp, user.myPort);
            IPEndPoint clientEndPoint = new IPEndPoint(user.ipClient, user.clientPort);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(myEndPoint);
            listener.Listen(10);
            while (!sender.Connected)
            {
                try
                {
                    sender.Connect(clientEndPoint);
                    Console.Clear();
                    Console.WriteLine("Подключение к {0} выполнено.", clientEndPoint);
                } catch
                {
                    Console.Clear();
                    Console.WriteLine("Ожидаем пользователя..."); 
                }
            }
            WorkServer(listener,sender);
        }

        static void WorkServer(Socket listener,Socket sender)
        {
            while (true)
            {               
                Socket server = listener.Accept();                
                Thread threadSend = new Thread(Send);
                threadSend.IsBackground = true;
                threadSend.Start(sender);
                Thread threadReceive = new Thread(Receive);
                threadReceive.IsBackground = true;
                threadReceive.Start(server);
                while (isClientOnline)
                {                    
                    queue.Add("client 2:" + Console.ReadLine());
                }
            }
        }                        
                        
        static string UnionString(string[] words)
        {
            string letter = "";
            for (int i = 0; i < words.Length; i++)
                letter += words[i] + " ";
            return letter;
        }

        static string ConvertMessage(String message)
        {
            string[] words = message.Split(' ');
            for(int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                string newWord = "";
                foreach (var element in dictionary)
                {
                    if (word.Contains(element.Key))
                    {
                        bool isSmthBfOrNt = true;                        
                        int indexStart = word.IndexOf(element.Key);
                        int indexEnd = element.Key.Length + indexStart - 1;
                        if (word.Length != element.Key.Length) 
                        {                            
                            if (indexStart != 0)
                                isSmthBfOrNt = Char.IsLetterOrDigit(word, indexStart - 1);
                            if (word.Length > indexEnd + 1)
                                isSmthBfOrNt = Char.IsLetterOrDigit(word, indexEnd + 1);
                        }
                        else
                            isSmthBfOrNt = false;
                        if (!isSmthBfOrNt)
                        {
                            for (int j = 0; j < indexStart; j++)
                                newWord += word[j];
                            newWord += element.Value;
                            for (int j = indexEnd + 1; j < word.Length; j++)
                                newWord += word[j];
                            words[i] = newWord;
                        }
                    }
                }
            }            
            return UnionString(words);
        }

        static void Send(object s)
        {
            Socket sender = (Socket)s;
            while (isClientOnline)
            {
                if (queue.Count > 0)
                {
                    byte[] sendMessage = Encoding.UTF8.GetBytes(queue[0]);
                    sender.Send(sendMessage);
                    queue.Remove(queue[0]);
                }                
            }
        }

        static void Receive(object s)
        {
            Socket server = (Socket)s;
            while (isClientOnline)
            {
                byte[] buffer = new byte[1024];
                try
                {
                    server.Receive(buffer);                    
                    string receiveMessage = "";
                    receiveMessage += Encoding.UTF8.GetString(buffer);
                    int value = receiveMessage.IndexOf('\0');
                    if (value != -1)
                        receiveMessage = receiveMessage.Substring(0, value);
                    receiveMessage = ConvertMessage(receiveMessage);                   
                    Console.WriteLine(receiveMessage);
                }
                catch 
                {
                    isClientOnline = false;
                    Console.WriteLine("Пользователь вышел из чата. ");                    
                }
            }
        }

        static void Main(string[] args)
        {
            dictionary.Add("негр", "черный пидорас");
            dictionary.Add("Негр", "Черный пидорас");
            Console.OutputEncoding = System.Text.Encoding.UTF8;            
            StartServer();
        }
    }
}
