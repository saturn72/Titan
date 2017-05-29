using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Calculator.Test.Framework
{
    public class TestUtil
    {
        public static string GetCurrentWorkingDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var path = Uri.UnescapeDataString(new UriBuilder(codeBase).Path);
            return Path.GetDirectoryName(path);
        }

        public static void KillAllWebDriversInstances()
        {
            var procs = Process.GetProcessesByName("chromedriver");
            foreach (var p in procs)
                p.Kill();

            Thread.Sleep(1000);
        }

        public static void StartCalcularotServer()
        {
            if (Process.GetProcessesByName("Calculator.WebApi.Host").Any())
                return;

            var sutExecutable = Path.Combine(GetCurrentWorkingDirectory(), "SUT\\Calculator.WebApi.Host.exe");

            var proc = Process.Start(sutExecutable);
            var startTime = DateTime.Now;

            do
            {
                Thread.Sleep(100);
                proc.Refresh();
            } while (string.IsNullOrEmpty(proc.MainWindowTitle) && startTime.Subtract(DateTime.Now).TotalSeconds < 5);
        }
    }
}