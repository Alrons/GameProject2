using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{

    public int Id;
    public string Title;
    public string Description;
    public int Price;
    public int �urrency;
    public string Image;
    public int Place;
    public int Health;
    public int Power;

    // �������, ��� 1 ��� true 2 ��� false ������������ ��� ��������, ��������� ��� ���
    public int XPover;

    public ItemModel(int Id, string Title, string Description, int Price, int �urrency, string Image, int Place, int Health, int Power,int XPover)
    {
        
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.Price = Price;
        this.�urrency = �urrency;
        this.Image = Image;
        this.Place = Place;
        this.Health = Health;
        this.Power = Power;
        this.XPover = XPover;  
    }
}
