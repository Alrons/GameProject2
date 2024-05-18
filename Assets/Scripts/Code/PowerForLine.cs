using System.Collections;
using System.Collections.Generic;
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
        spawnObject = MainCamera.GetComponent<SpawnObject> ();
        
    }

}
