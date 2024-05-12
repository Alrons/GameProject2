using Assets.Scripts.Class;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Refrash : MonoBehaviour
{
    public Transform RefrashCunvas;
    public GameObject PrefabObject;
    public GameObject MainCamera;

    public Text Title;
    public Text Price;
    public Text Description;
    public Text Health;
    public Text Power;
    public Text XPower;

    private List<AddedItemModel> addedItemsList = new List<AddedItemModel>();

    Tests tester = new Tests("SpawnObject");
    AddedItems addedItems = new AddedItems();
    Items Items = new Items();
    SizeTable SizeTable = new SizeTable();

    private SpawnObject spawnObject;

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
        foreach (AddedItemModel item in addedItemsList) 
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
        
        string AddedItemJson = await addedItems.Get();

        tester.StringLength(AddedItemJson, 10);

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
                Title.text = addedItemsList[JustNumber].title;
                Price.text = $"{addedItemsList[JustNumber].price}";
                Description.text = addedItemsList[JustNumber].description;
                Health.text = $"{addedItemsList[JustNumber].health}";
                Power.text = $"{addedItemsList[JustNumber].power}";
                XPower.text = $"{addedItemsList[JustNumber].xPower}";
                DragDrop script = spawnObject.CopyPref(PrefabObject, child.position, child).GetComponent<DragDrop>();
                script.ThisAddedItem = true;
                script.Id = addedItemsList[JustNumber].id;
                script.Title = addedItemsList[JustNumber].title;
                script.Description = addedItemsList[JustNumber].description;
                script.Price = addedItemsList[JustNumber].price;
                script.Ñurrency = addedItemsList[JustNumber].currency;
                script.Image = addedItemsList[JustNumber].image;
                script.Place = addedItemsList[JustNumber].place;
                script.Health = addedItemsList[JustNumber].health;
                script.Power = addedItemsList[JustNumber].power;
                script.XPower = addedItemsList[JustNumber].xPower;

            }

            Count++;
        }
    }
    

}
