using Assets.Scripts.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Class
{
    internal class SizeTable
    {
        private readonly HttpClient _httpClient;
        private string _url;
        public SizeTable()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/api/") };
            _url = "SizeTables/";
        }
        public async Task<string> Get()
        {
            try
            {
                var response = await _httpClient.GetAsync(_url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();

            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error getting added items: {ex.Message}");
                throw; // re-throw the exception
            }
        }
    }
}
