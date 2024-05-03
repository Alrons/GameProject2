using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    internal class AddedItems : IAddedItems
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
        public int XPover { get; set; }

        public string DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<ItemModel> GetItems()
        {
            throw new NotImplementedException();
        }

        public ItemModel GetItemByID(int id)
        {
            throw new NotImplementedException();
        }


        public string PostItem()
        {
            throw new NotImplementedException();
        }
    }
}
