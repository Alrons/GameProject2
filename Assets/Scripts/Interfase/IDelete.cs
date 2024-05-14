using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IDelete
{
    public Task<bool> Delete(int id);
}

