
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObject : MonoBehaviour
{
    private List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    private AddedItems addedItems;

    async void Start()
    {
        addedItems = new AddedItems();
        string json = await addedItems.GetAddedItems();
        SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
        for (int i = 0; i < stats.Count; i++)
        {
            addedItemsList.Add(new AddedItemModel
            (
                stats[i]["id"],
                stats[i]["userId"],
                stats[i]["title"],
                stats[i]["description"],
                stats[i]["price"],
                stats[i]["ñurrency"],
                stats[i]["image"],
                stats[i]["place"],
                stats[i]["health"],
                stats[i]["power"],
                stats[i]["xPover"]
            ));
        }
        Debug.Log(addedItemsList.Count);
        Debug.Log(addedItemsList);
    }
}
