
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Grid {

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;

    private GameObject[,] debugObjectArray;
    private GameObject GameObject;
    private Transform Transform;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, GameObject GameObject, Transform Transform) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.GameObject = GameObject;   
        this.Transform = Transform;

        gridArray = new int[width, height];

        bool showDebug = true;
        if (showDebug) {
            debugObjectArray = new GameObject[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugObjectArray[x,y] = CopyPref(GameObject, new Vector3(GetWorldPositionX(x).x,GetWorldPositionY(y).y) + new Vector3(cellSize, cellSize) * .5f, Transform);
                    
                }
            }
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    private GameObject CopyPref(GameObject box, Vector3 position, Transform setparent)
    {
        var spawn = UnityEngine.Object.Instantiate(box, position + new Vector3(20f,0), Quaternion.identity);
        spawn.transform.SetParent(setparent.transform);
        float y = (float)( (double)1 / (height + 1 ));
        float x = (float)( (double)1 / (width + 1 ));
        if (height == 1){y = 0.8f;}
        else if (height == 2){ y = 0.4f;}
        else if (height == 3){ y = 0.26f;}
        else if (height == 4){ y = 0.2f;}
        else if (height == 5) { y = 0.16f; }
        if (width == 1) { x = 0.8f; }
        else if (width == 2) { x = 0.4f; }
        else if (width == 3) { x = 0.26f; }
        else if (width == 4) { x = .2f; }
        else if (width == 5) { x = 0.16f; }

        spawn.transform.localScale = new Vector3(x, y);

        return spawn;

    }

    public Vector3 GetWorldPositionY(int y) {
        return new Vector3(0, y) * cellSize  + originPosition;
    }
    public Vector3 GetWorldPositionX( int x)
    {
        return new Vector3(x,0) * cellSize * 2 + originPosition;
    }

}
