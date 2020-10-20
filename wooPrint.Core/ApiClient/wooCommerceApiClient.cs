using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wooPrint.Core.ApiClient.Models;
using System.Diagnostics;
using System.Linq;

namespace wooPrint.Core.ApiClient
{
    /// <summary>
    ///
    /// </summary>
    public class wooCommerceApiClient
    {
        private static wooCommerceApiClient _instance;
        private const int ApiTimeOut = 30;

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        public static wooCommerceApiClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new wooCommerceApiClient();
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
                string url = Configuration.WooPrintConfiguration.Config().ApiService.Url;

                url = url.AppendPathSegment("orders")
                              .SetQueryParam("consumer_key", Configuration.WooPrintConfiguration.Config().ApiService.APIKey)
                              .SetQueryParam("consumer_secret", Configuration.WooPrintConfiguration.Config().ApiService.APISecret)
                              .SetQueryParam("status", "completed");

                if (!string.IsNullOrWhiteSpace(afterDate))
                    url = url.SetQueryParam("after", afterDate);

                var orders = await url.WithTimeout(ApiTimeOut)
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
                var notes = await Configuration.WooPrintConfiguration.Config().ApiService.Url
               .AppendPathSegment("orders/" + orderId + "/notes")
               .SetQueryParam("consumer_key", Configuration.WooPrintConfiguration.Config().ApiService.APIKey)
               .SetQueryParam("consumer_secret", Configuration.WooPrintConfiguration.Config().ApiService.APISecret)
               .SetQueryParam("status", "completed")
               .SetQueryParam("type", "internal")
               .WithTimeout(ApiTimeOut)
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
                var result = await Configuration.WooPrintConfiguration.Config().ApiService.Url
                    .AppendPathSegment("orders/" + orderId + "/notes")
                    .SetQueryParam("consumer_key", Configuration.WooPrintConfiguration.Config().ApiService.APIKey)
                    .SetQueryParam("consumer_secret", Configuration.WooPrintConfiguration.Config().ApiService.APISecret)
                    .WithTimeout(ApiTimeOut)
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