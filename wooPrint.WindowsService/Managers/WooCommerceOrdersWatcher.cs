using Quartz;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace wooPrint.WindowsService.Managers
{
    /// <summary>
    ///
    /// </summary>
    public class WooCommerceOrdersWatcher : IJob
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
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
                    Trace.TraceInformation("checking orders with status completed");
                    var completedOrders = await Core.ApiClient.wooCommerceApiClient.GetInstance().GetCompletedOrders();
                    if (completedOrders == null || completedOrders.Count == 0)
                    {
                        Trace.TraceInformation("no orders completed found.");
                        return;
                    }

                    await Task.Delay(1000 * 60);

                    // process order one to one
                    for (int i = 0; i < completedOrders.Count; i++)
                    {
                        var currOrder = completedOrders[i];

                        // print order
                        var printResult = PrintTickets(currOrder);
                        if (!printResult)
                        {
                            Trace.TraceWarning($"the roder with id={currOrder.id} cannot be printed.");
                            continue;
                        }

                        //set order as processed
                        await SetOrderProcessed(currOrder);
                    }
                };
                worker.RunWorkerAsync();
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static bool PrintTickets(Core.ApiClient.Models.Order orderInfo)
        {
            var printResult = Core.Utils.PrinterUtil.PrintTcket(orderInfo);
            if (string.IsNullOrWhiteSpace(printResult))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static Task SetOrderProcessed(Core.ApiClient.Models.Order orderInfo)
        {
            return null;
        }
    }
}