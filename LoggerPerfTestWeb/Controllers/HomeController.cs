using System;
using System.Diagnostics;
using System.Text;
using System.Web.Mvc;
using NLog;
using LogManager = Common.Logging.LogManager;

namespace LoggerPerfTestWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            GlobalDiagnosticsContext.Set("Application", "LoggerPerfTestWeb");
            var sb = new StringBuilder();

            var logger = LogManager.GetLogger("PerfTest");
            //var logger = LogManager.GetLogger("PerfTestCrash");
            var log4mb = System.IO.File.ReadAllText(Server.MapPath("~/bin/4MB.txt"));
            var nb4mb = 1000;
            long duration4mb;

            var sw = new Stopwatch();

            sb.AppendLine($"Start {nb4mb} 4MB logs.");

            sw.Start();
            for (int i = 0; i < nb4mb; i++)
            {
                logger.Info(log4mb);
            }
            sw.Stop();
            duration4mb = sw.ElapsedMilliseconds;

            sb.AppendLine($"{nb4mb} 4MB logs in {duration4mb}ms.");

            var proc = Process.GetCurrentProcess();
            sb.AppendLine($"PrivateMemorySize = {proc.PrivateMemorySize64}");
            sb.AppendLine($"VirtualMemorySize = {proc.VirtualMemorySize64}");
            sb.AppendLine($"PeakVirtualMemorySize = {proc.PeakVirtualMemorySize64}");
            sb.AppendLine($"PagedMemorySize = {proc.PagedMemorySize64}");
            sb.AppendLine($"PeakPagedMemorySize = {proc.PeakPagedMemorySize64}");

            return Content(sb.ToString());
        }
    }
}