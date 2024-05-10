using Assets.Scripts.Interfase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

    public class AddedItems : IGet
{
        private readonly HttpClient _httpClient;
        private string _url;

    public AddedItems()
    {
        _url = "https://localhost:7139/api/AddedItems/";
    }

    public async Task<bool> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_url+id);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Debug.LogError($"Error deleting item: {ex.Message}");
                throw; // re-throw the exception
            }
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

        public async Task<bool> Upload(AddedItemModel model)
        {
            try
            {
                var json = JsonUtility.ToJson(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url, content);
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
