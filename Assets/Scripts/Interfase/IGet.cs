using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfase
{
    public interface IGet
    {
        public Task<string> Get();
    }
}
