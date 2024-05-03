using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfase
{
    public interface IUpdate
    {
        public string UpdateAll();
        public string UpdateShop ();
        public string UpdatePlace();

        // string return result of update
    }
}
