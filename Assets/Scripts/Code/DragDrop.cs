using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform recetTransform;
    private Vector2 startPos;// ��������� �������
    private bool posNow;

    public GameObject form;// ����� ���������� � ������� �� ����� ��������� ����� ��� �������� ��������

    public GameObject dragObject; // ��� ������
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

    public void OnBeginDrag(PointerEventData eventData)//������� 
    {

        scrollRect.vertical = false;
        //image.raycastTarget = false;
        startPos = dragObject.transform.position; // ����� ���������� ����������� ������� � ����������
    }

    public void OnDrag(PointerEventData eventData)// ����������� 
    {
        recetTransform = GetComponent<RectTransform>();
        recetTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)//��������� 
    {
        scrollRect.vertical = true;

        this.transform.position = startPos;// ����������� �� ����� ���� ������� �� ����� 
    }


}
