using System;
using System.Diagnostics;
using System.IO;
using NLog;
using LogManager = Common.Logging.LogManager;

namespace LoggerPerfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalDiagnosticsContext.Set("Application", "LoggerPerfTest");

            var logger = LogManager.GetLogger("PerfTest");
            //var logger = LogManager.GetLogger("PerfTestCrash");
            var log4mb = File.ReadAllText("4MB.txt");
            var nb4mb = 1000;
            long duration4mb;

            var sw = new Stopwatch();

            Console.WriteLine($"Start {nb4mb} 4MB logs.");

            sw.Start();
            for (int i = 0; i < nb4mb; i++)
            {
                logger.Info(log4mb);
            }
            sw.Stop();
            duration4mb = sw.ElapsedMilliseconds;

            Console.WriteLine($"{nb4mb} 4MB logs in {duration4mb}ms.");

            var proc = Process.GetCurrentProcess();
            Console.WriteLine($"PrivateMemorySize = {proc.PrivateMemorySize64}");
            Console.WriteLine($"VirtualMemorySize = {proc.VirtualMemorySize64}");
            Console.WriteLine($"PeakVirtualMemorySize = {proc.PeakVirtualMemorySize64}");
            Console.WriteLine($"PagedMemorySize = {proc.PagedMemorySize64}");
            Console.WriteLine($"PeakPagedMemorySize = {proc.PeakPagedMemorySize64}");

            Console.ReadKey();
        }
    }
}
