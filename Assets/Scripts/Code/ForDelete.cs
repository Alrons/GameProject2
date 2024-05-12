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


    private void Start()
    {
        DragDropScript = GameObjects.GetComponent<DragDrop>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int Count = 1;
        Debug.Log(DragDropScript.ThisAddedItem);
        if (DragDropScript.ThisAddedItem)
        {

            Items items = new Items();
            AddedItems addedItems = new AddedItems();
            addedItems.Delete(DragDropScript.Id);
            items.Upload(new ItemModel(DragDropScript.Id, DragDropScript.Title, DragDropScript.Description, DragDropScript.Price, DragDropScript.Сurrency, DragDropScript.Image, DragDropScript.Place, DragDropScript.Health, DragDropScript.Power, DragDropScript.XPower));
            Destroy(GameObjects);
        }
        
    }
}

