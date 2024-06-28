using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    internal class itemServiceProperties
    {
        // Use a constant for the base URL
        public readonly string BaseUrl = "https://localhost:7139/api/";

        // Use a readonly field for the HTTP client
        public HttpClient _httpClient {  get; set; }

        // Use a readonly field for the URL
        public string _AddedItemsUrl { get; set; }

        public string _Items { get; set; }

        public string _SizeTables { get; set; }

        public string _OurTables { get; set; }

        public itemServiceProperties()
        {
            // Initialize the HTTP client
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            // Initialize the URL
            _AddedItemsUrl = "AddedItems/";
            _Items = "Items/";
            _SizeTables = "SizeTables/";
            _OurTables = "OurTables/";
        }
    }
}
