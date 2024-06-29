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

    // ��� ��� ������ ���������� � 1 ��������, ����� �������� ��������� ������� ��������
    public bool ThisAddedItem { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int �urrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }
    public int XPower { get; set; }

    // �������� ����� �������
    public GameObject dragObject; // ��� ������
    public ScrollRect scrollRect;
    public Transform CanfasWereDrop;
    public GameObject SciptSpawnObject;
    public GameObject SciptRefresh;
    public GameObject coins;



    dragDropProperties dragDropProperties = new dragDropProperties();
    Tests tester = new Tests("DragDrop");


    // ����� ��� ����� ��������
    private SpawnObject MainCamera;
    private Refrash Refrash;
    private currency currency;

    private void Start()
    {
        // ... other initialization code ...
        MainCamera = SciptSpawnObject.GetComponent<SpawnObject>();
        Refrash = SciptSpawnObject.GetComponent<Refrash>();
        currency = coins.GetComponent<currency>();

    }

    private void FormIsFull()
    {
        if (dragDropProperties.form.transform.childCount > 0)
        {
            dragDropProperties. formIsFull = false;
        }
        else
        {
            dragDropProperties.formIsFull = true;
        }
    }


    private void FindForm()
    {
        if (!dragDropProperties.DidTheFormSearchWork)
        {
            TableCreator tableCreator = SciptSpawnObject.GetComponent<TableCreator>();
            dragDropProperties.form = tableCreator.ourCell[Place - 1];
            dragDropProperties.DidTheFormSearchWork = true;
        }
        

    }

    //ShopItemModel shopItemModel = spawnObject.shopItemModels;
   

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == dragDropProperties.form)
        {
            Debug.Log("collision: works");
            dragDropProperties.posNow = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == dragDropProperties.form)
        {
            dragDropProperties.posNow = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)//������� 
    {

        FindForm();
        scrollRect.vertical = false;
        //image.raycastTarget = false;
        dragDropProperties.startPos = dragObject.transform.position; // ����� ���������� ����������� ������� � ����������
        dragDropProperties.form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.6f);//���������




    }

    public void OnDrag(PointerEventData eventData)// ����������� 
    {
        dragDropProperties.recetTransform = GetComponent<RectTransform>();
        dragDropProperties.recetTransform.anchoredPosition += eventData.delta;
    }


    //��������� ���� ����� N ���������� ������� 
    IEnumerator CantUseForm() 
    {

        dragDropProperties.form.GetComponent<Image>().color = new Color(255f, 0f, 0f, 0.2f);
        yield return new WaitForSeconds(1);
        dragDropProperties.form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);
    }

    public async void OnEndDrag(PointerEventData eventData)//��������� 
    {
        FormIsFull();
        scrollRect.vertical = true;
        bool Check = false;
        if (dragDropProperties.posNow)
        {
            if (dragDropProperties.formIsFull)
            {
                if (currency.currencyValues[�urrency-1]>= Price)
                {
                    ItemService itemService = new ItemService();
                    currency.currencyValues[�urrency - 1] = currency.currencyValues[�urrency - 1] - Price;

                    bool check = await itemService.PostAddedItem(new AddedItemModel(Id, 1, Title, Description, Price, �urrency, Image, Place, Health, Power, XPower));
                    Refreshing(check);

                    Destroy(dragObject);
                }
                else
                {
                    Check = true;
                }

            }

        }
        dragDropProperties.form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);//���������
        if (Check)
        {
            StartCoroutine(CantUseForm());
        }
        this.transform.position = dragDropProperties.startPos;// ����������� �� ����� ���� ������� �� ����� 
    }
    private async void Refreshing(bool check)
    {
        ItemService itemService = new ItemService();
        if (check)
        {
            if (await itemService.DeleteItem(Id))
            {
                if (await Refrash.RefreshPlaseforDrop())
                {
                    Refrash.RefreshItemsInShop();
                }
               
            }
        }
        
        
    }


}
