using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;


    public interface IAddedItems : IItems{

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

