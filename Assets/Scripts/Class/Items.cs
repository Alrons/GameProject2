using Assets.Scripts.Interfase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Items: IGet, IDelete, IUploadItem
{
    private readonly HttpClient _httpClient;

    private string _url;
    public Items()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/api/") };
        _url = "Items/";
    }
    public ItemModel GetItemByID(int id)
    {
        throw new System.NotImplementedException();
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
    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync("https://localhost:7139/api/Items/" + id);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error deleting item: {ex.Message}");
            throw; // re-throw the exception
        }
    }
    public async Task<bool> Upload(ItemModel model)
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
