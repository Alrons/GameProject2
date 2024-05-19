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

        Task<string> GetAddedItem();

        Task<string> GetSizeTable();

        Task<bool> PostItem(ItemModel model);

        Task<bool> PostAddedItem(AddedItemModel model);

        Task<bool> DeleteItem(int id);

        Task<bool> DeleteAddedItem(int id);
    }
}
