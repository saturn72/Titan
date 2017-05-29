using System;
using Calculator.WebApi.Host.Configure;
using Microsoft.Owin.Hosting;

namespace Calculator.WebApi.Host
{
    public class Program
    {
        private static void Main()
        {
            var baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Running Web Api Host. Press any key to exit");
                Console.ReadKey();
                Console.WriteLine("Exiting...");
            }
        }
    }
}