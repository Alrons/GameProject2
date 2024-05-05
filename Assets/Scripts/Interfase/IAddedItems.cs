using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;


    public interface IAddedItems : IItems{

    public int UserId {  get; set; }
    public Task<int> CreateItem(ItemModel item)
    {
        try
        {
            var url = "https://localhost:7139/api/Items";

            var json = JsonUtility.ToJson(item);
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var itemId = JsonUtility.FromJson<ItemIdResponse>(request.downloadHandler.text).Id;
                return Task.FromResult(itemId);
            }
            else
            {
                throw new Exception($"Failed to create item: {request.result}");
            }
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error creating item: {ex.Message}");
            return Task.FromResult(-1); // or throw an exception
        }
    }

    [System.Serializable]
    public class ItemIdResponse
    {
        public int Id;
    }
    public async Task<bool> DeleteAddedItem(int id)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7139/api/");

            var response = await httpClient.DeleteAsync($"AddedItems/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error deleting item: {ex.Message}");
            return false;
        }
    }
    // return string request result 

}

