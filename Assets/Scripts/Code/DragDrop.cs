using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform recetTransform;
    private Vector2 startPos;// стартовая позиция
    
    private GameObject form;// общая переменная в которую мы будем назначать место для большего удобства
    private bool posNow;
    private bool formIsFull;
    private bool DidTheFormSearchWork = false;
    private bool DidThePlaseSearchWork = false;
    private int OurPlase;

    private int OurID;
    private int userId;
    private string title;
    private string description;
    private int price;
    private int currency;
    private string image;
    private int place;
    private int health;
    private int power;
    private int xPower;

    [SerializeField] private GameObject dragObject; // наш объект
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform CanfasWereDrop;
    public GameObject SciptSpawnObject;

    Tests tester = new Tests("DragDrop");
    private SpawnObject spawnObject;

    private void Start()
    {
        // ... other initialization code ...
        spawnObject = SciptSpawnObject.GetComponent<SpawnObject>();
        Debug.Log(spawnObject);
    }

    private void FormIsFull()
    {
        if (form.transform.childCount > 0)
        {
            formIsFull = false;
        }
        else
        {
            formIsFull = true;
        }
    }


    private void FindForm()
    {
        if (!DidTheFormSearchWork)
        {
            int count = 1;
            foreach (Transform child in CanfasWereDrop.transform)
            {

                if (child.name == "PlaceForDrop(Clone)")
                {
                    if (count == OurPlase)
                    {
                        Debug.Log("FindForm: OK");
                        form = child.gameObject;
                    }
                    count++;
                }
            }
            DidTheFormSearchWork = true;
        }
        

    }
    //ShopItemModel shopItemModel = spawnObject.shopItemModels;
    private void FindOurPlase(SpawnObject spawnObject)
    {
        if (!DidThePlaseSearchWork) {
            bool ForlustItem = true;
            for (int i = 0; i < spawnObject.shopItemModels.Count; i++)
            {
                if (spawnObject.shopItemModels[i].GameObject == dragObject)
                {
                    OurPlase = spawnObject.shopItemModels[i].place;
                    userId = spawnObject.shopItemModels[i].userId;
                    OurID = spawnObject.shopItemModels[i].id;
                    title = spawnObject.shopItemModels[i].title;
                    description = spawnObject.shopItemModels[i].description;
                    price = spawnObject.shopItemModels[i].price;
                    currency = spawnObject.shopItemModels[i].currency;
                    image = spawnObject.shopItemModels[i].image;
                    place = spawnObject.shopItemModels[i].place;
                    health = spawnObject.shopItemModels[i].health;
                    power = spawnObject.shopItemModels[i].power;
                    xPower = spawnObject.shopItemModels[i].xPower;
                    ForlustItem = false;
                }

            }
            if (ForlustItem)
            {
                Debug.Log("ForlustItem: OK");
                OurPlase = spawnObject.shopItemModels[^1].place;
                userId = spawnObject.shopItemModels[^1].userId;
                OurPlase = spawnObject.shopItemModels[^1].place;
                OurID = spawnObject.shopItemModels[^1].id;
                title = spawnObject.shopItemModels[^1].title;
                description = spawnObject.shopItemModels[^1].description;
                price = spawnObject.shopItemModels[^1].price;
                currency = spawnObject.shopItemModels[^1].currency;
                image = spawnObject.shopItemModels[^1].image;
                place = spawnObject.shopItemModels[^1].place;
                health = spawnObject.shopItemModels[^1].health;
                power = spawnObject.shopItemModels[^1].power;
                xPower = spawnObject.shopItemModels[^1].xPower;

            }

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == form)
        {
            Debug.Log("collision: works");
            posNow = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == form)
        {
            posNow = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)//подняте 
    {
        FindOurPlase(spawnObject);
        FindForm();
        scrollRect.vertical = false;
        //image.raycastTarget = false;
        startPos = dragObject.transform.position; // Берем координаты изначальной позиций и запоминаем
    }

    public void OnDrag(PointerEventData eventData)// перемещение 
    {
        recetTransform = GetComponent<RectTransform>();
        recetTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)//опускание 
    {
        FormIsFull();
        scrollRect.vertical = true;
        if (posNow)
        {
            if (formIsFull)
            {
                Items items = new Items();
                AddedItems addedItems = new AddedItems();

                items.Delete(OurID);
                addedItems.Upload(new AddedItemModel(OurID, userId, title, description,price,currency,image,place,health,power,xPower));

                Destroy(dragObject);


            }
            
        }

        this.transform.position = startPos;// возвращение на место если условие не верно 
    }


}
