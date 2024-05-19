using Assets.Scripts.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Class
{
    public class ItemService : IItemService
    {
        // Use a constant for the base URL
        private const string BaseUrl = "https://localhost:7139/api/";

        // Use a readonly field for the HTTP client
        private readonly HttpClient _httpClient;

        // Use a readonly field for the URL
        private readonly string _AddedItemsUrl;

        private readonly string _Items;

        private readonly string _SizeTables;

        // Use a private field for the Refrash component

        public ItemService()
        {
            // Initialize the HTTP client
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            // Initialize the URL
            _AddedItemsUrl = "AddedItems/";
            _Items = "Items/";
            _SizeTables = "SizeTables/";
        }
        public async Task<bool> DeleteAddedItem(int id)
        {
            try
            {
                // Use the BaseUrl constant
                var response = await _httpClient.DeleteAsync($"{BaseUrl}{_AddedItemsUrl}{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error deleting item: {ex.Message}");
                throw; // re-throw the exception
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}{_Items}{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error deleting item: {ex.Message}");
                throw; // re-throw the exception
            }
        }

        public async Task<string> GetAddedItem()
        {
            try
            {
                var response = await _httpClient.GetAsync(_AddedItemsUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error getting added items: {ex.Message}");
                throw; // re-throw the exception
            }
        }

        public async Task<string> GetItem()
        {
            try
            {
                var response = await _httpClient.GetAsync(_Items);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();

            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error getting added items: {ex.Message}");
                throw; // re-throw the exception
            }
        }

        public async Task<string> GetSizeTable()
        {
            try
            {
                var response = await _httpClient.GetAsync(_SizeTables);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();

            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error getting added items: {ex.Message}");
                throw; // re-throw the exception
            }
        }

        public async Task<bool> PostAddedItem(AddedItemModel model)
        {
            try
            {
                var json = JsonUtility.ToJson(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_AddedItemsUrl, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Debug.Log("Response: " + responseBody);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                throw; // re-throw the exception
            }
        }

        public async Task<bool> PostItem(ItemModel model)
        {
            try
            {
                var json = JsonUtility.ToJson(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_Items, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Debug.Log("Response: " + responseBody);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
                throw; // re-throw the exception
            }
        }
    }
}
