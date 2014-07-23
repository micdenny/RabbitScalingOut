using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.NonGeneric;
using RabbitScalingOut.Contract;

namespace RabbitScalingOut.Producer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus();

            string messageVersion = ConfigurationManager.AppSettings["MessageVersion"];
            int sleep = int.Parse(ConfigurationManager.AppSettings["Sleep"]);

            var stopped = false;
            var thread = new Thread(() =>
            {
                while (!stopped)
                {
                    if (messageVersion == "1")
                    {
                        bus.Publish(new MyMessage1());
                    }

                    if (messageVersion == "2")
                    {
                        bus.Publish(new MyMessage2());
                    }

                    if (!stopped) Thread.Sleep(sleep);
                }
            });
            thread.Start();

            Console.WriteLine("Press enter to exit the application...");
            Console.ReadLine();

            stopped = true;
            thread.Join();

            bus.Dispose();
        }
    }
}