using Assets.Scripts.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Class
{
    public class ItemService : IItemService
    {
        public Task<bool> AddItem(AddedItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateItem(ItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetItem()
        {
            throw new NotImplementedException();
        }
    }
}
