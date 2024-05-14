using Assets.Scripts.Class;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Refrash : MonoBehaviour
{
    public Transform RefrashCunvas;
    public GameObject PrefabObject;
    public GameObject MainCamera;
    public GameObject ContentShop;

    public Text Title;
    public Text Price;
    public Text Description;
    public Text Health;
    public Text Power;
    public Text XPower;

    private SpawnObject spawnObject;

    Tests tester = new Tests("SpawnObject");
    AddedItems addedItems = new AddedItems();
    Items Items = new Items();
    SizeTable SizeTable = new SizeTable();


    public void Start()
    {
        spawnObject = MainCamera.GetComponent<SpawnObject>();
        foreach (Transform child in MainCamera.transform)
        {
            if (child.name == "Canvas Place")
            {
                Debug.Log("1111111");
                RefrashCunvas = child;
                if (RefrashCunvas == null)
                {
                    Debug.Log("íîëü");
                }
            }
        }

    }


    private int ForThisPlase(int count)
    {
        int counting = 0;
        foreach (AddedItemModel item in spawnObject.addedItemsList) 
        {
            if (count == item.place)
            {
                return counting;
            }
            counting++;
        }
        return -1;
    }

    public async void RefreshPlaseforDrop()
    {
        spawnObject.addedItemsList.Clear(); 
        string AddedItemJson = await addedItems.Get();

        tester.StringLength(AddedItemJson, 10);

        SimpleJSON.JSONNode AddedItemstats = SimpleJSON.JSON.Parse(AddedItemJson);
        for (int i = 0; i < AddedItemstats.Count; i++)
        {
            spawnObject.addedItemsList.Add(new AddedItemModel
            (
                AddedItemstats[i]["id"],
                AddedItemstats[i]["userId"],
                AddedItemstats[i]["title"],
                AddedItemstats[i]["description"],
                AddedItemstats[i]["price"],
                AddedItemstats[i]["ñurrency"],
                AddedItemstats[i]["image"],
                AddedItemstats[i]["place"],
                AddedItemstats[i]["health"],
                AddedItemstats[i]["power"],
                AddedItemstats[i]["xPover"]
            ));
        }
        int Count = 1;
        foreach (var child in from Transform child in RefrashCunvas
                              where child.name == "PlaceForDrop(Clone)"
                              select child)
        {
            
            foreach (Transform children in child)
            {
                Destroy(children.gameObject);
            }

            int JustNumber = ForThisPlase(Count);
            if (JustNumber == -1) ;
            else
            {
                if (spawnObject.addedItemsList[JustNumber].place <= (spawnObject.sizeTable[^1].height * spawnObject.sizeTable[^1].width))
                {
                    Title.text = spawnObject.addedItemsList[JustNumber].title;
                    Price.text = $"{spawnObject.addedItemsList[JustNumber].price}";
                    Description.text = spawnObject.addedItemsList[JustNumber].description;
                    Health.text = $"{spawnObject.addedItemsList[JustNumber].health}";
                    Power.text = $"{spawnObject.addedItemsList[JustNumber].power}";
                    XPower.text = $"{spawnObject.addedItemsList[JustNumber].xPower}";
                    DragDrop script = spawnObject.CopyPref(PrefabObject, child.position, child).GetComponent<DragDrop>();
                    script.ThisAddedItem = true;
                    script.Id = spawnObject.addedItemsList[JustNumber].id;
                    script.Title = spawnObject.addedItemsList[JustNumber].title;
                    script.Description = spawnObject.addedItemsList[JustNumber].description;
                    script.Price = spawnObject.addedItemsList[JustNumber].price;
                    script.Ñurrency = spawnObject.addedItemsList[JustNumber].currency;
                    script.Image = spawnObject.addedItemsList[JustNumber].image;
                    script.Place = spawnObject.addedItemsList[JustNumber].place;
                    script.Health = spawnObject.addedItemsList[JustNumber].health;
                    script.Power = spawnObject.addedItemsList[JustNumber].power;
                    script.XPower = spawnObject.addedItemsList[JustNumber].xPower;
                }
            }

            Count++;
        }
    }
    public async void RefreshItemsInShop()
    {
        string ItemJson = await Items.Get();

        tester.StringLength(ItemJson, 10);

        spawnObject.AllItems.Clear();

        SimpleJSON.JSONNode Itemstats = SimpleJSON.JSON.Parse(ItemJson);
        for (int i = 0; i < Itemstats.Count; i++)
        {
            spawnObject.AllItems.Add(new ItemModel
            (
                Itemstats[i]["id"],
                Itemstats[i]["title"],
                Itemstats[i]["description"],
                Itemstats[i]["price"],
                Itemstats[i]["ñurrency"],
                Itemstats[i]["image"],
                Itemstats[i]["place"],
                Itemstats[i]["health"],
                Itemstats[i]["power"],
                Itemstats[i]["xPover"]
            ));

        }
        foreach (Transform child in ContentShop.transform)
        {
            Destroy(child.gameObject);
            
        }
        int count = 0;
        foreach (ItemModel Item in spawnObject.AllItems)
        {
            if (spawnObject.AllItems[count].Place <= (spawnObject.sizeTable[^1].height * spawnObject.sizeTable[^1].width))
            {
                Title.text = Item.Title;
                Price.text = $"{Item.Price}";
                Description.text = Item.Description;
                Health.text = $"{Item.Health}";
                Power.text = $"{Item.Power}";
                XPower.text = $"{Item.XPover}";

                GameObject gmItem = spawnObject.CopyPref(PrefabObject, new Vector3(0, 0, 0), ContentShop.transform);
                DragDrop dragDrop = gmItem.GetComponent<DragDrop>();
                dragDrop.Id = spawnObject.AllItems[count].Id;
                dragDrop.Title = spawnObject.AllItems[count].Title;
                dragDrop.Description = spawnObject.AllItems[count].Description;
                dragDrop.Price = spawnObject.AllItems[count].Price;
                dragDrop.Ñurrency = spawnObject.AllItems[count].Ñurrency;
                dragDrop.Image = spawnObject.AllItems[count].Image;
                dragDrop.Place = spawnObject.AllItems[count].Place;
                dragDrop.Health = spawnObject.AllItems[count].Health;
                dragDrop.Power = spawnObject.AllItems[count].Power;
                dragDrop.XPower = spawnObject.AllItems[count].XPover;
            }
            count++;
        }
    } 
}
