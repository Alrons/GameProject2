using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IUploadItem
{
    public Task<bool> Upload(ItemModel model);
}

