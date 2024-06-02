
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
                    dragDrop.Сurrency = AllItems[i].Сurrency;
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
            spawn.GetComponent<PowerForLine>().NumberLine = i;
            spawn.GetComponent<PowerForLine>().Сalculating_line_capacity();
        }
        Destroy(PlaseDrop);
        Destroy(ForLine);
        Debug.Log(ourTables.Count);
        foreach (OurTablesModel Tables in ourTables)
        {
            CreateTableWithDrag(Tables.Width, Tables.Height, 40f,0, new Vector3((float)Tables.PosX, (float)Tables.PosY, 0), plasePrefub, plase);
        }

    }

    // ширина, высота, размер ячейки, поворот в градусах, где спавним, префаб ячейки, префаб задней стенки
    private void CreateTableWithDrag(int width, int height, float cellSize, int degrees, Vector3 SpawnPoint, GameObject plasePrefub, Transform plase)
    {
        GameObject OurBackTable = CopyPrefForTables(plase.gameObject, SpawnPoint, MainCamera.transform);

        OurBackTable.transform.localScale = new Vector3(width * cellSize * 2, height * cellSize);

        Vector3 position = OurBackTable.transform.position;
        Vector3 scale = OurBackTable.transform.localScale;
        Quaternion rotation = OurBackTable.transform.rotation;

        // Рассчитаем нижний левый угол объекта в локальных координатах
        Vector3 bottomLeftLocal = new Vector3(-0.5f * scale.x, -0.5f * scale.y, -0.5f * scale.z);

        Vector3 bottomLeftWorld = rotation * bottomLeftLocal;

        // Добавим позицию объекта
        bottomLeftWorld += position;

        grid = new Grid(width, height, cellSize, bottomLeftWorld, plasePrefub, OurBackTable.transform);
        // поворот в градусах
        OurBackTable.transform.Rotate(0, 0, degrees);
    }
    public GameObject CopyPrefForTables(GameObject box, Vector3 position, Transform setparent)
    {
        var spawn = UnityEngine.Object.Instantiate(box, position, Quaternion.identity);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(10f, (float)0.35, (float)0.35);

        return spawn;

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
                        if (addedItemsList[i].place <= (sizeTable[^1].height * sizeTable[^1].width))
                        {
                            Transform ItemAddedBox = CopyPref(Box, child.transform.position, child).transform;
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = $"{addedItemsList[i].title}";
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = $"{addedItemsList[i].health}";
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[2].text = $"{addedItemsList[i].xPower}";
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[3].text = $"{addedItemsList[i].price}";
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[4].text = $"{addedItemsList[i].power}";
                            ItemAddedBox.GetComponentsInChildren<UnityEngine.UI.Text>()[5].text = $"{addedItemsList[i].description}";

                            DragDrop script = ItemAddedBox.gameObject.GetComponent<DragDrop>();
                            script.ThisAddedItem = true;
                            script.Id = addedItemsList[i].id;
                            script.Title = addedItemsList[i].title;
                            script.Description = addedItemsList[i].description;
                            script.Price = addedItemsList[i].price;
                            script.Сurrency = addedItemsList[i].currency;
                            script.Image = addedItemsList[i].image;
                            script.Place = addedItemsList[i].place;
                            script.Health = addedItemsList[i].health;
                            script.Power = addedItemsList[i].power;
                            script.XPower = addedItemsList[i].xPower;
                            break;
                        }

                            
                    }
                }
                count++;
            }
            
        }
    }



}
