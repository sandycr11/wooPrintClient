using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using wooPrint.DesktopApp.ApiClient.Models;

namespace wooPrint.DesktopApp.ApiClient
{
    /// <summary>
    ///
    /// </summary>
    public class WooCommerceApiClient
    {
        private static WooCommerceApiClient _instance;

        /// <summary>
        ///
        /// </summary>
        private WooCommerceApiClient()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        public static WooCommerceApiClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WooCommerceApiClient();
            }
            return _instance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetCompletedOrders(string afterDate = null)
        {
            try
            {
                string url = Configuration.ConfigurationManager.GetInstance().Config.ApiUrl;

                url = url.AppendPathSegment("orders")
                              .SetQueryParam("consumer_key", Configuration.ConfigurationManager.GetInstance().Config.ApiKey)
                              .SetQueryParam("consumer_secret", Configuration.ConfigurationManager.GetInstance().Config.ApiSecret)
                              .SetQueryParam("status", "completed");

                if (!string.IsNullOrWhiteSpace(afterDate))
                    url = url.SetQueryParam("after", afterDate);

                var orders = await url.WithTimeout(Configuration.ConfigurationManager.GetInstance().Config.ApiTimeout)
                    .GetJsonAsync<List<Order>>();

                return orders;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> IsOrderProcessed(int orderId)
        {
            try
            {
                var notes = await Configuration.ConfigurationManager.GetInstance().Config.ApiUrl
               .AppendPathSegment("orders/" + orderId + "/notes")
               .SetQueryParam("consumer_key", Configuration.ConfigurationManager.GetInstance().Config.ApiKey)
               .SetQueryParam("consumer_secret", Configuration.ConfigurationManager.GetInstance().Config.ApiSecret)
               .SetQueryParam("status", "completed")
               .SetQueryParam("type", "internal")
               .WithTimeout(Configuration.ConfigurationManager.GetInstance().Config.ApiTimeout)
               .GetJsonAsync<List<Note>>();

                var allNotes = notes.ToArray();

                return allNotes.Any(n => n.note.Equals("Procesado", StringComparison.InvariantCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> SetOrderProcessed(int orderId)
        {
            try
            {
                var result = await Configuration.ConfigurationManager.GetInstance().Config.ApiUrl
                    .AppendPathSegment("orders/" + orderId + "/notes")
                    .SetQueryParam("consumer_key", Configuration.ConfigurationManager.GetInstance().Config.ApiKey)
                    .SetQueryParam("consumer_secret", Configuration.ConfigurationManager.GetInstance().Config.ApiSecret)
                    .WithTimeout(Configuration.ConfigurationManager.GetInstance().Config.ApiTimeout)
                    .PostJsonAsync(new
                    {
                        note = "Procesado"
                    }).ReceiveJson<Note>();

                if (result != null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }
    }
}