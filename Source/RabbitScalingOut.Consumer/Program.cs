using System;
using System.Configuration;
using EasyNetQ;
using RabbitScalingOut.Contract;

namespace RabbitScalingOut.Consumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus();

            string messageVersion = ConfigurationManager.AppSettings["MessageVersion"];

            if (messageVersion == "1")
            {
                bus.Subscribe<MyMessage1>("Test", message => { });
            }

            if (messageVersion == "2")
            {
                bus.Subscribe<MyMessage2>("Test", message => { });
            }

            Console.WriteLine("Press enter to exit the application...");
            Console.ReadLine();

            bus.Dispose();
        }
    }
}