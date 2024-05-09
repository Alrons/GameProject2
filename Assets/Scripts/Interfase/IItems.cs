using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItems{ 
    public List<ItemModel> GetItems();
    public ItemModel GetItemByID(int id);
}
