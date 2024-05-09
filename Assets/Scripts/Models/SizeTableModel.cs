using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class SizeTableModel
    {
        public int height;
        public int width;

        public SizeTableModel (int height, int width)
        {
            this.height = height;
            this.width = width;
        }
    }
}
