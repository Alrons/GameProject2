using Assets.Scripts.Class;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
    ItemService ItemService = new ItemService();


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
    public void RefreshLinePower()
    {
        TableCreator tableCreator = GetComponent<TableCreator>();
        foreach (Text txt in tableCreator.textsLinePower)
        {
            PowerForLine powerForLine = txt.GetComponent<PowerForLine>();
            powerForLine.CulculateLine();
        }
    }

    private int FindTableNumber(int NumberCell)
    {
        int totalCells = 0;
        foreach (var table in spawnObject.ourTables)
        {
            int tableCells = table.Height * table.Width;
            if (totalCells <= NumberCell && NumberCell <= totalCells + tableCells)
            {
                return table.Id;
            }
            totalCells += tableCells;
        }
        return -1; // èëè throw exception, åñëè òàáëèöà íå íàéäåíà
    }

    public async Task<bool> RefrachLists()
    {
        string AddedItemJson = await ItemService.GetAddedItem();

        tester.StringLength(AddedItemJson, 10);

        spawnObject.addedItemsList = JsonConvert.DeserializeObject<List<AddedItemModel>>(AddedItemJson);

        string ItemJson = await ItemService.GetItem();

        tester.StringLength(ItemJson, 10);

        spawnObject.AllItems = JsonConvert.DeserializeObject<List<ItemModel>>(ItemJson);

        return true;
    }
   

    public async Task<bool> RefreshPlaseforDrop()
    {
    
        if(await RefrachLists())
        {
            
            TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
            int Count = 1;
            foreach (GameObject gameobj in tableCreator.ourCell)
            {

                foreach (Transform children in gameobj.transform)
                {
                    Destroy(children.gameObject);
                }

                for (int i = 0; i < spawnObject.addedItemsList.Count; i++)
                {
                    if (spawnObject.addedItemsList[i].place == Count)
                    {
                        Title.text = spawnObject.addedItemsList[i].title;
                        Price.text = $"{spawnObject.addedItemsList[i].price}";
                        Description.text = spawnObject.addedItemsList[i].description;
                        Health.text = $"{spawnObject.addedItemsList[i].health}";
                        Power.text = $"{spawnObject.addedItemsList[i].power}";
                        XPower.text = $"{spawnObject.addedItemsList[i].xPower}";
                        GameObject _object = spawnObject.CopyPref(PrefabObject, gameobj.transform.position, gameobj.transform);
                        DragDrop script = _object.GetComponent<DragDrop>();
                        script.ThisAddedItem = true;
                        script.Id = spawnObject.addedItemsList[i].id;
                        script.Title = spawnObject.addedItemsList[i].title;
                        script.Description = spawnObject.addedItemsList[i].description;
                        script.Price = spawnObject.addedItemsList[i].price;
                        script.Ñurrency = spawnObject.addedItemsList[i].currency;
                        script.Image = spawnObject.addedItemsList[i].image;
                        script.Place = spawnObject.addedItemsList[i].place;
                        script.Health = spawnObject.addedItemsList[i].health;
                        script.Power = spawnObject.addedItemsList[i].power;
                        script.XPower = spawnObject.addedItemsList[i].xPower;


                        _object.transform.Rotate(0, 0, (float)spawnObject.ourTables[FindTableNumber(Count) - 1].Rotate);


                        break;
                    }
                    
                }
                Count++;

            }
            RefreshLinePower();
        }
        
        return true;
    }
        
    

    public async Task<bool> RefreshItemsInShop()
    {
        TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
        if (await RefrachLists())
        {

            foreach (Transform child in ContentShop.transform)
            {
                Destroy(child.gameObject);

            }
            int count = 0;
            foreach (ItemModel Item in spawnObject.AllItems)
            {
                if (spawnObject.AllItems[count].Place <= tableCreator.ourCell.Count)
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
            RefreshLinePower();
        }
        return true;
    } 
}
