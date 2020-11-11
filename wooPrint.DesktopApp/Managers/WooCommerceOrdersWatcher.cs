﻿using Quartz;
using System;
using System.ComponentModel;
using System.Diagnostics;
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
                    if (string.IsNullOrWhiteSpace(Configuration.ConfigurationManager.GetInstance().Config.ApiUrl)
                        || string.IsNullOrWhiteSpace(Configuration.ConfigurationManager.GetInstance().Config.ApiKey)
                        || string.IsNullOrWhiteSpace(Configuration.ConfigurationManager.GetInstance().Config.ApiSecret))
                    {
                        Trace.TraceInformation("invalid configuration. not processing");
                        return;
                    }

                    Trace.TraceInformation("checking orders with status completed");

                    string cachedDate = Configuration.ConfigurationManager.GetInstance().Config.LastOrderChecked;

                    var completedOrders = await ApiClient.WooCommerceApiClient.GetInstance().GetCompletedOrders(cachedDate);
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

                        var isProcessed = await ApiClient.WooCommerceApiClient.GetInstance().IsOrderProcessed(currOrder.id);
                        if (isProcessed)
                            continue;

                        // print order
                        var printResult = Utils.PrinterUtil.PrintTcket(currOrder);
                        if (string.IsNullOrWhiteSpace(printResult))
                        {
                            var addResult = await ApiClient.WooCommerceApiClient.GetInstance().SetOrderProcessed(currOrder.id);
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

                    cachedDate = completedOrders[0].date_created.Subtract(TimeSpan.FromDays(7)).ToString("yyyy-MM-dd HH:mm:ss");
                    Configuration.ConfigurationManager.GetInstance().Config.LastOrderChecked = cachedDate;
                    Configuration.ConfigurationManager.GetInstance().Save();
                };
                worker.RunWorkerAsync();
            });
        }
    }
}