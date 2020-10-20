using Quartz;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wooPrint.DesktopApp.Managers
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class WooCommerceOrdersWatcher : IJob
    {
        private static NotifyIcon _notifier;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            _notifier = (NotifyIcon)(context.JobDetail.JobDataMap.Get("notifier"));
            return CheckForCompletedOrders();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private static Task CheckForCompletedOrders()
        {
            return Task.Run(() =>
            {
                var worker = new BackgroundWorker();
                worker.DoWork += async (sender, eventArgs) =>
                {
                    // checking configuration params
                    if (string.IsNullOrWhiteSpace(Core.Configuration.WooPrintConfiguration.Config().ApiService.Url)
                        || string.IsNullOrWhiteSpace(Core.Configuration.WooPrintConfiguration.Config().ApiService.APIKey)
                        || string.IsNullOrWhiteSpace(Core.Configuration.WooPrintConfiguration.Config().ApiService.APISecret))
                    {
                        Trace.TraceInformation("invalid configuration. not processing");
                        return;
                    }

                    Trace.TraceInformation("checking orders with status completed");

                    string cachedDate = ReadProcessedDate();

                    var completedOrders = await Core.ApiClient.wooCommerceApiClient.GetInstance().GetCompletedOrders(cachedDate);
                    if (completedOrders == null || completedOrders.Count == 0)
                    {
                        Trace.TraceInformation("no orders completed found.");
                        return;
                    }
                    else
                    {
                        Trace.TraceInformation($"found {completedOrders.Count} orders. processing.");
                    }

                    // process order one to one
                    for (int i = completedOrders.Count - 1; i >= 0; i--)
                    {
                        var currOrder = completedOrders[i];

                        var isProcessed = await Core.ApiClient.wooCommerceApiClient.GetInstance().IsOrderProcessed(currOrder.id);
                        if (isProcessed)
                            continue;

                        // print order
                        var printResult = Core.Utils.PrinterUtil.PrintTcket(currOrder);
                        if (string.IsNullOrWhiteSpace(printResult))
                        {
                            var addResult = await Core.ApiClient.wooCommerceApiClient.GetInstance().SetOrderProcessed(currOrder.id);
                            if (!addResult)
                            {
                                Trace.TraceWarning($"the order with id = {currOrder.id} cannot set processed.");
                                _notifier.ShowBalloonTip(1000, @"WooCommerce Printer", $"The order with ID " + currOrder.id + " cannot set processed.", ToolTipIcon.Warning);
                            }
                            else
                            {
                                Trace.TraceInformation($"the order with id = {currOrder.id} was processed successfully.");
                                _notifier.ShowBalloonTip(1000, @"WooCommerce Printer", $"The order with ID " + currOrder.id + " was processed successfully.", ToolTipIcon.Info);
                            }
                        }
                        else
                        {
                            Trace.TraceWarning($"the order with id = {currOrder.id} cannot be printed.");
                            _notifier.ShowBalloonTip(1000, @"WooCommerce Printer", $"The order with ID " + currOrder.id + " cannot be printed.", ToolTipIcon.Warning);
                        }
                    }

                    cachedDate = completedOrders[0].date_created.ToString("yyyy-MM-dd HH:mm:ss");
                    WriteProcessedDate(cachedDate);
                };
                worker.RunWorkerAsync();
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string ReadProcessedDate()
        {
            try
            {
                var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
                string assemblyPath = new FileInfo(location.AbsolutePath).Directory.FullName;

                var filePath = Path.Combine(assemblyPath, "cache.data");
                if (!File.Exists(filePath))
                    return string.Empty;

                string cachedDate = string.Empty;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    cachedDate = sr.ReadLine();
                }

                return cachedDate;
            }
            catch (System.Exception ex)
            {
                Trace.TraceInformation(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        private static void WriteProcessedDate(string date)
        {
            try
            {
                var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
                string assemblyPath = new FileInfo(location.AbsolutePath).Directory.FullName;

                var filePath = Path.Combine(assemblyPath, "cache.data");

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(date);
                }
            }
            catch (System.Exception ex)
            {
                Trace.TraceInformation(ex.ToString());
            }
        }
    }
}