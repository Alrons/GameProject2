using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class AddedItem
    {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Сurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }

    // Костыль, где 1 это true 2 это false
    public int XPover { get; set; }

    public AddedItem(int Id, int UserId, string Title, string Description, int Price, int Сurrency, string Image, int Place, int Health, int Power, int v)
    {

        this.Id = Id;
        this.Title = Title;
        this.UserId = UserId;
        this.Description = Description;
        this.Price = Price;
        this.Сurrency = Сurrency;
        this.Image = Image;
        this.Place = Place;
        this.Health = Health;
        this.Power = Power;
    }
}

