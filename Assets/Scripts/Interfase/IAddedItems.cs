using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IAddedItems : IItems{

    public string PostItem();
    public string DeleteItem(int id);
    // return string request result 

}

