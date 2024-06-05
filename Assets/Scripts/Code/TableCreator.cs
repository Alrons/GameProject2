
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class TableCreator : MonoBehaviour
{
    public Transform Canvas;
    public Transform cellBackgroundTransform; // Трансформ объекта фона ячейки
    public Transform cellContentTransform; // Трансформ объекта содержимого ячейки
    private RectTransform rectTransfrom;

    public List<GameObject> ourCell = new List<GameObject>();

    public int cellSpacing = 10; // Расстояние между ячейками

    public void CreateTable(int width, int height, Vector3 Position, int Rotate)
    {
        // Создаем фон таблицы
        GameObject gameObject = CopyPref(cellBackgroundTransform.gameObject);
        gameObject.transform.SetParent(Canvas);
        gameObject.transform.position = Position;

        // Создаем таблицу
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // Создаем копию ячейки
                GameObject cellBackground = CopyPref(cellContentTransform.gameObject);

                // сохраняем нашу ячейку, ее порядок создания будет определять ее номер
                ourCell.Add(cellBackground);

                cellBackground.transform.localScale = new Vector3(1, 1, 1); // изменяем размер ячейки
                cellBackground.transform.SetParent(gameObject.transform);

                cellBackground.transform.position = gameObject.transform.position + new Vector3(j * (100 + cellSpacing), -i * (30 + cellSpacing), 0); // изменяем позицию ячейки

            }
        }

        gameObject.transform.localScale = new Vector3(1, 1, 1);
        rectTransfrom = gameObject.GetComponent<RectTransform>();
        rectTransfrom.sizeDelta = new Vector2(((width - 1) * 200) + 170, ((height - 1) * 130) + 75);
        gameObject.transform.Rotate(0, 0, Rotate);
    }

    //то что копируем \ куда копируем x y z \ родитель
    public GameObject CopyPref(GameObject box)
    {
        var spawn = Instantiate(box);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;

    }
}