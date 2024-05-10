using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ShopItemModel
    {
        public int id;
        public int userId;
        public string title;
        public string description;
        public int price;
        public int currency;
        public string image;
        public int place;
        public int health;
        public int power;
        public int xPower;
        public GameObject GameObject;

       
        public ShopItemModel(int id, int userId, string title, string description, int price, int currency, string image, int place, int health, int power, int xPower, GameObject GameObject) 
        {
        
            this.id = id;
            this.userId = userId;
            this.title = title;
            this.description = description;
            this.price = price;
            this.currency = currency;
            this.image = image;
            this.place = place;
            this.health = health;
            this.power = power;
            this.xPower = xPower;
            this.GameObject = GameObject;
           
        }
        
    }
}
