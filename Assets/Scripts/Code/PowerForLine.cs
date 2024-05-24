using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PowerForLine : MonoBehaviour
{
    public GameObject MainCamera;
    public Text OneLine;
    public int NumberLine {  get; set; }

    private SpawnObject spawnObject;
    public void Ñalculating_line_capacity () 
    {
        int OurPower = 0;
        int CountLine = 0;
        
        spawnObject = MainCamera.GetComponent<SpawnObject> ();
        for(int i = 0; i < spawnObject.sizeTable[^1].width * spawnObject.sizeTable[^1].height; i++)
        {
            if (CountLine-1 == NumberLine)
            {
                
                for (int j = 0; j < spawnObject.addedItemsList.Count; j++)
                {
                    if (i == spawnObject.addedItemsList[j].place)
                    {
                        OurPower += spawnObject.addedItemsList[j].power;
                    }
                }
            }
            if (i % spawnObject.sizeTable[^1].width == 0)
            {
                CountLine++;
                
            }
        }
        OneLine.text = $"{OurPower}";

    }

}
