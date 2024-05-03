using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform recetTransform;
    private Vector2 startPos;// стартовая позиция
    private bool posNow;

    public GameObject form;// общая переменная в которую мы будем назначать место для большего удобства

    public GameObject dragObject; // наш объект
    public ScrollRect scrollRect;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == form)
        {
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

        scrollRect.vertical = false;
        //image.raycastTarget = false;
        startPos = dragObject.transform.position; // Берем коарденаты изначальной позиций и запоминаем
    }

    public void OnDrag(PointerEventData eventData)// перемещение 
    {
        recetTransform = GetComponent<RectTransform>();
        recetTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)//опускание 
    {
        scrollRect.vertical = true;

        this.transform.position = startPos;// возвращение на место если условие не верно 
    }


}
