
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Assets.Scripts.Class;
using Assets.Scripts.Models;

public class SpawnObject : MonoBehaviour
{
    private List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    private List<ItemModel> itemModels = new List<ItemModel>();
    private List<SizeTableModel> sizeTable = new List<SizeTableModel>();
    private int count;

    public GameObject Box;
    public Transform CanvasObject;

    public GameObject PlaseDrop;
    public Transform CanvasPlaseDrop;
    public Text ForLine;

    public Text Title;
    public Text Price;
    public Text Description;
    public Text Health;
    public Text Power;
    public Text XPower;

    private Text Title1;
    private Text Price1;
    private Text Description1;
    private Text Health1;
    private Text Power1;
    private Text XPower1;
    Tests tester = new Tests("SpawnObject");
    AddedItems addedItems = new AddedItems();
    Items Items = new Items();
    SizeTable SizeTable = new SizeTable(); 
    async void Start()
    {

        string AddedItemJson = await addedItems.Get();

        tester.StringLength(AddedItemJson, 1, 10);

        SimpleJSON.JSONNode AddedItemstats = SimpleJSON.JSON.Parse(AddedItemJson);
        for (int i = 0; i < AddedItemstats.Count; i++)
        {
            addedItemsList.Add(new AddedItemModel
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
        
        string ItemJson = await Items.Get();

        tester.StringLength(ItemJson, 10);

        SimpleJSON.JSONNode Itemstats = SimpleJSON.JSON.Parse(ItemJson);
        for (int i = 0; i < Itemstats.Count; i++)
        {
            itemModels.Add(new ItemModel
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
        string SizeTableJson = await SizeTable.Get();

        tester.StringLength(SizeTableJson, 10);

        SimpleJSON.JSONNode SizeTablestats = SimpleJSON.JSON.Parse(SizeTableJson);
        for (int i = 0; i < SizeTablestats.Count; i++)
        {
            sizeTable.Add(new SizeTableModel
            (
                SizeTablestats[i]["height"],
                SizeTablestats[i]["width"]
                
            ));
        }
        Initializing();


    }

    private void Initializing()
    {
        InitializingItems();
        InitializingTable();
        InitializingAddedItems();
    }
    public GameObject CopyPref(GameObject box, Vector3 position, Transform setparent)
    {
        var spawn = Instantiate(box, position, Quaternion.identity);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(1, 1, 1);

        return spawn;

    }
    private void ChangePref(string title, int price, string description, int health, double power, double xpower){
        Title.text = title;
        Price.text = String.Format("{0}", price);
        Description.text = description;
        Health.text = String.Format("{0}", health);
        Power.text = String.Format("{0}", power);
        XPower.text = String.Format("{0}", xpower);
    }
    
    private void InitializingItems()
    {
        tester.IntRange(itemModels.Count, 2);
        for (int i = 0; i < itemModels.Count; i++)
        {
            tester.ForStart("InitializingItems",i, itemModels.Count);
            if (count != itemModels.Count)
            {
                ChangePref(itemModels[i].Title, itemModels[i].Price, itemModels[i].Description, itemModels[i].Health, itemModels[i].Power, itemModels[i].XPover);
                CopyPref(Box, Box.transform.position, CanvasObject);
            }
            else
            {
                ChangePref(itemModels[i].Title, itemModels[i].Price, itemModels[i].Description, itemModels[i].Health, itemModels[i].Power, itemModels[i].XPover);
            }
        }
    }
    private void InitializingTable()
    {
        float x = (float)(PlaseDrop.transform.position.x * -0.8);
        float y = (float)(PlaseDrop.transform.position.y * -0.5);
        tester.Print($"{sizeTable[0].width}");
        tester.IntRange(sizeTable[0].width, 2);
        tester.IntRange(sizeTable[0].height, 2);
        for (int i = 0; i < sizeTable[0].width; i++)
        {
            tester.ForStart("InitializingItems", i, sizeTable[0].width);
            int l = 0;
            for (int j = 0; j < sizeTable[0].height; j++)
            {
                CopyPref(PlaseDrop, new Vector2(PlaseDrop.transform.position.x + x * j, PlaseDrop.transform.position.y + y * i), CanvasPlaseDrop);
                l = j + 1;
            }
            var spawn = Instantiate(ForLine, new Vector3(PlaseDrop.transform.position.x + x * l, PlaseDrop.transform.position.y + y * i, PlaseDrop.transform.position.z), Quaternion.identity);
            spawn.transform.SetParent(CanvasPlaseDrop.transform);
            spawn.transform.localScale = new Vector3(1, 1, 1);
        }

        Destroy(PlaseDrop);
        Destroy(ForLine);
    }
    private void InitializingAddedItems()
    {

        int count = 1;
        foreach (Transform child in CanvasPlaseDrop.transform)
        {
            
            if (child.name == "PlaceForDrop(Clone)")
            {
                for (int i = 0; i < addedItemsList.Count; i++)
                {
                    if (count == addedItemsList[i].place)
                    {
                        Transform ItemAddedBox = CopyPref(Box, child.transform.position, child).transform;
                        ItemAddedBox.GetComponentsInChildren<Text>()[0].text = $"{addedItemsList[i].title}";
                        ItemAddedBox.GetComponentsInChildren<Text>()[1].text = $"{addedItemsList[i].health}";
                        ItemAddedBox.GetComponentsInChildren<Text>()[2].text = $"{addedItemsList[i].xPower}";
                        ItemAddedBox.GetComponentsInChildren<Text>()[3].text = $"{addedItemsList[i].price}";
                        ItemAddedBox.GetComponentsInChildren<Text>()[4].text = $"{addedItemsList[i].power}";
                        ItemAddedBox.GetComponentsInChildren<Text>()[5].text = $"{addedItemsList[i].description}";
                        break;
                    }
                }
                tester.Print($"{child.name}");
                count++;
            }
            
        }
    }
}
