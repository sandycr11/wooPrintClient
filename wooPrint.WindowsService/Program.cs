using System.Diagnostics;
using System.ServiceProcess;

namespace wooPrint.WindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            // load configuration (cache strategy)
            var _ = Core.Configuration.WooPrintConfiguration.Config().ApiService;

            //create logger in application events
            Trace.Listeners.Add(new EventLogTraceListener(new EventLog
            {
                Log = "wooPrint",
                Source = "WooCommerce Print Service"
            }));

            // run service
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new wooPrintService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}