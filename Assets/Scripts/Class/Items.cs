using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : IItems
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Ñurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }
    public int XPover { get; set; }

    public ItemModel GetItemByID(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<ItemModel> GetItems()
    {
        throw new System.NotImplementedException();
    }
}
