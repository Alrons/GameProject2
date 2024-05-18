using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfase
{
    public interface IItemService
    {
        Task<string> GetItem();

        Task<bool> CreateItem(ItemModel model);

        Task<bool> AddItem(AddedItemModel model);

        Task<bool> DeleteItem(int id);
    }
}
