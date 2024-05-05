
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private void Start()
    {

        AddedItem itemModel = new AddedItem(1,1, "string", "string", 10,100, "string",1000,10000,2,2);
        AddedItems addedItems = new AddedItems();
        Debug.Log(addedItems.CreateItem(itemModel));
    }
}
