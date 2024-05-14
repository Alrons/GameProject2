using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool ThisAddedItem { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Сurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }
    public int XPower { get; set; }

    private RectTransform recetTransform;
    private Vector2 startPos;// стартовая позиция
    
    private GameObject form;// общая переменная в которую мы будем назначать место для большего удобства
    private bool posNow;
    private bool formIsFull;
    private bool DidTheFormSearchWork = false;
    private bool DidThePlaseSearchWork = false;
    private int OurPlase;

    [SerializeField] private GameObject dragObject; // наш объект
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform CanfasWereDrop;
    public GameObject SciptSpawnObject;
    public GameObject SciptRefresh;




    Tests tester = new Tests("DragDrop");
    private SpawnObject spawnObject;
    private Refrash Refrash;

    private void Start()
    {
        // ... other initialization code ...
        spawnObject = SciptSpawnObject.GetComponent<SpawnObject>();
        Refrash = SciptSpawnObject.GetComponent<Refrash>();
        

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
                    if (count == Place)
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

        FindForm();
        scrollRect.vertical = false;
        //image.raycastTarget = false;
        startPos = dragObject.transform.position; // Берем координаты изначальной позиций и запоминаем
        form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.3f);//подсветка




    }

    public void OnDrag(PointerEventData eventData)// перемещение 
    {
        recetTransform = GetComponent<RectTransform>();
        recetTransform.anchoredPosition += eventData.delta;
    }

    public async void OnEndDrag(PointerEventData eventData)//опускание 
    {
        FormIsFull();
        scrollRect.vertical = true;
        if (posNow)
        {
            if (formIsFull)
            {
                Items items = new Items();
                AddedItems addedItems = new AddedItems();


                bool check = await addedItems.Upload(new AddedItemModel(Id, 1, Title, Description,Price,Сurrency,Image,Place,Health,Power,XPower));
                Refreshing(check);

                Destroy(dragObject);


            }
            
        }
        form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);//подсветка

        this.transform.position = startPos;// возвращение на место если условие не верно 
    }
    private async void Refreshing(bool check)
    {
        Items items = new Items();
        if (check)
        {
            if (await items.Delete(Id))
            {
                Refrash.RefreshPlaseforDrop();
            }
        }
        
        
    }


}
