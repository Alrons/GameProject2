using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Сurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }

    // Костыль, где 1 это true 2 это false используется для проверки, добавленн или нет
    public int XPover { get; set; }

    public ItemModel(int Id, string Title, string Description, int Price, int Сurrency, string Image, int Place, int Health, int Power,int XPover)
    {
        
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.Price = Price;
        this.Сurrency = Сurrency;
        this.Image = Image;
        this.Place = Place;
        this.Health = Health;
        this.Power = Power;
        this.XPover = XPover;  
    }
}
