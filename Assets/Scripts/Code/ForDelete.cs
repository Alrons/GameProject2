using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;


public class ForDelete : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameObjects;
    public GameObject MainCamera;
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
        Debug.Log(DragDropScript.ThisAddedItem);
        if (DragDropScript.ThisAddedItem)
        {

            Items items = new Items();
            AddedItems addedItems = new AddedItems();
            addedItems.Delete(DragDropScript.Id);
            bool Cheak = await items.Upload(new ItemModel(DragDropScript.Id, DragDropScript.Title, DragDropScript.Description, DragDropScript.Price, DragDropScript.Сurrency, DragDropScript.Image, DragDropScript.Place, DragDropScript.Health, DragDropScript.Power, DragDropScript.XPower));
            if (Cheak)
            {
                refrash.RefreshItemsInShop();
            }
            
            Destroy(GameObjects);
        }
        
    }
}

