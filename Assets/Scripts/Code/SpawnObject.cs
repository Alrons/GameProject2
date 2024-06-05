
using System.Collections;
using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using SimpleJSON;



public class SpawnObject : MonoBehaviour
{
    public List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    public List<AddedItemsModelTest> Test = new List<AddedItemsModelTest>();
    public List<ItemModel> AllItems = new List<ItemModel>();
    public List<SizeTableModel> sizeTable = new List<SizeTableModel>();
    public List<OurTablesModel> ourTables = new List<OurTablesModel>();

    private int count;

    public GameObject Box;
    public Transform CanvasObject;

    public GameObject PlaseDrop;
    public Transform CanvasPlaseDrop;

    public UnityEngine.UI.Text ForLine;
    public UnityEngine.UI.Text Title;
    public UnityEngine.UI.Text Price;
    public UnityEngine.UI.Text Description;
    public UnityEngine.UI.Text Health;
    public UnityEngine.UI.Text Power;
    public UnityEngine.UI.Text XPower;

    
    Tests tester = new Tests("SpawnObject");
    ItemService ItemService = new ItemService();
    private Grid grid;
    public GameObject MainCamera;
    public GameObject plasePrefub;
    public Transform plase;
    async void Start()
    {
        string AddedItemJson = await ItemService.GetAddedItem();

        tester.StringLength(AddedItemJson, 10);

        addedItemsList = JsonConvert.DeserializeObject<List<AddedItemModel>>(AddedItemJson);
        


        string ItemJson = await ItemService.GetItem();

        AllItems = JsonConvert.DeserializeObject<List<ItemModel>>(ItemJson);

        tester.StringLength(ItemJson, 10);



        string SizeTableJson = await ItemService.GetSizeTable();

        tester.StringLength(SizeTableJson, 10);

        sizeTable = JsonConvert.DeserializeObject<List<SizeTableModel>>(SizeTableJson);



        string OurTablseJson = await ItemService.GetOurTables();

        tester.StringLength(OurTablseJson, 10);

        ourTables = JsonConvert.DeserializeObject<List<OurTablesModel>>(OurTablseJson);

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
        tester.IntRange(AllItems.Count, 2);
        for (int i = 0; i < AllItems.Count; i++)
        {
            tester.ForStart("InitializingItems",i, AllItems.Count);
            if (count != AllItems.Count)
            {
                if (AllItems[i].Place <= (sizeTable[^1].height * sizeTable[^1].width))
                {
                    ChangePref(AllItems[i].Title, AllItems[i].Price, AllItems[i].Description, AllItems[i].Health, AllItems[i].Power, AllItems[i].XPover);
                    GameObject gmItem = CopyPref(Box, Box.transform.position, CanvasObject);
                    DragDrop dragDrop = gmItem.GetComponent<DragDrop>();
                    dragDrop.Id = AllItems[i].Id;
                    dragDrop.Title = AllItems[i].Title;
                    dragDrop.Description = AllItems[i].Description;
                    dragDrop.Price = AllItems[i].Price;
                    dragDrop.Ñurrency = AllItems[i].Ñurrency;
                    dragDrop.Image = AllItems[i].Image;
                    dragDrop.Place = AllItems[i].Place;
                    dragDrop.Health = AllItems[i].Health;
                    dragDrop.Power = AllItems[i].Power;
                    dragDrop.XPower = AllItems[i].XPover;
                }
                
            }
           
        }
    }
    private void InitializingTable()
    {
        TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
        foreach (OurTablesModel Tables in ourTables)
        {
            tableCreator.CreateTable(Tables.Width, Tables.Height,new Vector3 ((float)Tables.PosX, (float)Tables.PosY),0);
        }

    }


    private void InitializingAddedItems()
    {

        TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
        int Count = 1;
        foreach (GameObject gameobj in tableCreator.ourCell)
        {

            foreach (Transform children in gameobj.transform)
            {
                Destroy(children.gameObject);
            }

            for (int i = 0; i < addedItemsList.Count; i++)
            {
                if (addedItemsList[i].place == Count)
                {
                    Title.text = addedItemsList[i].title;
                    Price.text = $"{addedItemsList[i].price}";
                    Description.text = addedItemsList[i].description;
                    Health.text = $"{addedItemsList[i].health}";
                    Power.text = $"{addedItemsList[i].power}";
                    XPower.text = $"{addedItemsList[i].xPower}";
                    DragDrop script = CopyPref(Box, gameobj.transform.position, gameobj.transform).GetComponent<DragDrop>();
                    script.ThisAddedItem = true;
                    script.Id = addedItemsList[i].id;
                    script.Title = addedItemsList[i].title;
                    script.Description = addedItemsList[i].description;
                    script.Price = addedItemsList[i].price;
                    script.Ñurrency = addedItemsList[i].currency;
                    script.Image = addedItemsList[i].image;
                    script.Place = addedItemsList[i].place;
                    script.Health = addedItemsList[i].health;
                    script.Power = addedItemsList[i].power;
                    script.XPower = addedItemsList[i].xPower;
                    break;
                }

            }
            Count++;

        }
    }



}
