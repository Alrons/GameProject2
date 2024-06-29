using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class dragDropProperties
{
    public RectTransform recetTransform { get; set; }

    public Vector2 startPos { get; set; } // стартовая позиция, по этой позиций item будет возврашаться если не попадет в форму

    public GameObject form { get; set; }// общая переменная в которую мы будем назначать физическое место (в которое вставляется предмет)

    public bool posNow { get; set; }

    public bool formIsFull { get; set; }

    public bool DidTheFormSearchWork { get; set; }

    public dragDropProperties()
    {
        DidTheFormSearchWork = false;
    }
}

