using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PowerForLine : MonoBehaviour
{
    
    public int StartNumberCell {  get; set; }
    public int EndNumberCell { get; set; }

    private int Power = 0;

    public Text OurLineText;

    public GameObject MainCamera;
    public bool CulculateLine()
    {
        Power = 0;
        for (int i = StartNumberCell; i < EndNumberCell; i++)
        {
            TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
            GameObject cell = tableCreator.ourCell[i];
            foreach (Transform item in cell.transform)
            {
                DragDrop dragDrop = item.GetComponent<DragDrop>();
                Power += dragDrop.Power;
            }
        }
        OurLineText.text = $"{Power}";
        return true;
    }
}
