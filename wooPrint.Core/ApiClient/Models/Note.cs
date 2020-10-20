using System;

namespace wooPrint.Core.ApiClient.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Note
    {
        public int id { get; set; }
        public string author { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_created_gmt { get; set; }
        public string note { get; set; }
        public bool customer_note { get; set; }
    }
}