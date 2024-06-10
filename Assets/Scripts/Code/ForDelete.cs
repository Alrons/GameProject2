using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using Assets.Scripts.Class;


public class ForDelete : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameObjects;
    public GameObject MainCamera;
    public GameObject Coins;
    private DragDrop DragDropScript;
    private Refrash refrash;


    private void Start()
    {
        DragDropScript = GameObjects.GetComponent<DragDrop>();
        refrash = MainCamera.GetComponent<Refrash>();
    }
    public async void OnPointerClick(PointerEventData eventData)
    {
        int Count = 1;
        Debug.Log(DragDropScript.Сurrency);
        if (DragDropScript.ThisAddedItem)
        {
            Coins.GetComponent<currency>().currencyValues[DragDropScript.Сurrency - 1] = Coins.GetComponent<currency>().currencyValues[DragDropScript.Сurrency - 1] + (DragDropScript.Price/2);
            Coins.GetComponent<currency>().ChangeValues();
            ItemService ItemService = new ItemService();
            ItemService.DeleteAddedItem(DragDropScript.Id);
            bool Cheak = await ItemService.PostItem(new ItemModel(DragDropScript.Id, DragDropScript.Title, DragDropScript.Description, DragDropScript.Price, DragDropScript.Сurrency, DragDropScript.Image, DragDropScript.Place, DragDropScript.Health, DragDropScript.Power, DragDropScript.XPower));
            
            if (Cheak)
            {
                refrash.RefreshItemsInShop();
                refrash.RefreshLinePower();
                
                
            }
            
            Destroy(GameObjects);
        }
        
    }
}

