using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using UnityEngine.Networking;
using UnityEngine;
using static UnityEditor.Progress;


    public class AddedItems : IAddedItems
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Сurrency { get; set; }
        public string Image { get; set; }
        public int Place { get; set; }
        public int Health { get; set; }
        public int Power { get; set; }
        public int XPover { get; set; }


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

        public List<ItemModel> GetItems()
        {
            throw new NotImplementedException();
        }

        public ItemModel GetItemByID(int id)
        {
            throw new NotImplementedException();
        }

    public async Task<int> CreateItem(AddedItem item)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7139/api/");

            var json = JsonUtility.ToJson(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("AddedItems", content);

            if (response.IsSuccessStatusCode)
            {
                var itemId = JsonUtility.FromJson<AddedItemResponse>(await response.Content.ReadAsStringAsync()).Id;
                return itemId;
            }
            else
            {
                throw new Exception($"Failed to create item: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error creating item: {ex.Message}");
            return -1; // or throw an exception
        }
    }
}
[System.Serializable]
public class AddedItemResponse
{
    public int Id;
}

