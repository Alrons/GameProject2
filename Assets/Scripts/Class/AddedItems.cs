using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

    public class AddedItems
    {
        private readonly HttpClient _httpClient;
        private List<AddedItemModel> addedItems;

    public AddedItems()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/api/") };
    }

    public async Task<bool> DeleteAddedItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"AddedItems/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error deleting item: {ex.Message}");
                throw; // re-throw the exception
            }
        }

    public async Task<string> GetAddedItems()
    {
        try
        {
            var response = await _httpClient.GetAsync("AddedItems");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting added items: {ex.Message}");
            throw; // re-throw the exception
        }
    }

        public async Task<bool> Upload(AddedItemModel model)
        {
            try
            {
                var json = JsonUtility.ToJson(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("AddedItems", content);
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
