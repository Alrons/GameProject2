using Assets.Scripts.Interfase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class AddedItems {

    // Use a constant for the base URL
    private const string BaseUrl = "https://localhost:7139/api/";

    // Use a readonly field for the HTTP client
    private readonly HttpClient _httpClient;

    // Use a readonly field for the URL
    private readonly string _url;

    // Use a private field for the Refrash component

    public AddedItems()
    {
        // Initialize the HTTP client
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

        // Initialize the URL
        _url = "AddedItems/";
    }


    public async Task<bool> Delete(int id)
    {
        try
        {
            // Use the BaseUrl constant
            var response = await _httpClient.DeleteAsync($"{BaseUrl}{_url}{id}");
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