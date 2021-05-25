using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using wooPrint.DesktopApp.ApiClient.Models;
using wooPrint.DesktopApp.Configuration;

namespace wooPrint.DesktopApp.ApiClient
{
    /// <summary>
    /// </summary>
    public class WooCommerceApiClient
    {
        private static WooCommerceApiClient _instance;

        /// <summary>
        /// </summary>
        private WooCommerceApiClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static WooCommerceApiClient GetInstance()
        {
            return _instance ?? (_instance = new WooCommerceApiClient());
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetCompletedOrders(string afterDate = null)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.GetInstance().Config.ApiUrl)
                {
                    Timeout = ConfigurationManager.GetInstance().Config.ApiTimeout * 1000
                };

                var getOrdersRequest = new RestRequest("orders");
                getOrdersRequest.AddQueryParameter("consumer_key", ConfigurationManager.GetInstance().Config.ApiKey);
                getOrdersRequest.AddQueryParameter("consumer_secret", ConfigurationManager.GetInstance().Config.ApiSecret);
                getOrdersRequest.AddQueryParameter("status", "completed");

                if (!string.IsNullOrWhiteSpace(afterDate))
                    getOrdersRequest.AddQueryParameter("after", afterDate);

                var ordersResponse = await client.ExecuteAsync<List<Order>>(getOrdersRequest);
                var orders = ordersResponse.Data;
                
                return orders;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> IsOrderProcessed(int orderId)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.GetInstance().Config.ApiUrl)
                {
                    Timeout = ConfigurationManager.GetInstance().Config.ApiTimeout * 1000
                };

                var getNotesRequest = new RestRequest("orders/{orderId}/notes");
                getNotesRequest.AddUrlSegment("orderId", orderId);

                getNotesRequest.AddQueryParameter("consumer_key", ConfigurationManager.GetInstance().Config.ApiKey);
                getNotesRequest.AddQueryParameter("consumer_secret", ConfigurationManager.GetInstance().Config.ApiSecret);
                getNotesRequest.AddQueryParameter("status", "completed");
                getNotesRequest.AddQueryParameter("type", "internal");
                
                var notesResponse = await client.ExecuteAsync<List<Note>>(getNotesRequest);
                var allNotes = notesResponse.Data;

                var isProcessed = allNotes.Any(n => n.note.Equals("Procesado", StringComparison.InvariantCultureIgnoreCase));

                return isProcessed;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> SetOrderProcessed(int orderId)
        {
            try
            {
                var client = new RestClient(ConfigurationManager.GetInstance().Config.ApiUrl)
                {
                    Timeout = ConfigurationManager.GetInstance().Config.ApiTimeout * 1000
                };

                var postNoteRequest = new RestRequest("orders/{orderId}/notes", Method.POST, DataFormat.Json);
                postNoteRequest.AddUrlSegment("orderId", orderId);

                postNoteRequest.AddQueryParameter("consumer_key", ConfigurationManager.GetInstance().Config.ApiKey);
                postNoteRequest.AddQueryParameter("consumer_secret", ConfigurationManager.GetInstance().Config.ApiSecret);

                postNoteRequest.AddJsonBody(new
                {
                    note = "Procesado"
                });

                var notesResponse = await client.ExecuteAsync<Note>(postNoteRequest);
                var noteResult = notesResponse.Data;

                return noteResult != null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }
    }
}