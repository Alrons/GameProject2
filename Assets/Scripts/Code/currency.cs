using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// � ���������� ������ ����� �� ������ ����, � ����� ���� ��� ���������������� � ������� spawnObject � ��������� � ������ ����

public class currency : MonoBehaviour
{
    // ��� ���� ����� �������� ����, ����� ������� �������� �������� � ������, ����� ��������� ChangeValues()
    public List<string> currencyNames = new List<string>();
    public List<int> currencyValues = new List<int>();

    public GameObject MainCamera;
    public Transform Canvas;
    public GameObject Pref;
    public Text textCurrencyNames;
    public Text textcurrencyValues;


    private void Start()
    {
        currencyNames.Add("currency1");
        currencyNames.Add("currency2");
        currencyNames.Add("currency3");

        currencyValues.Add(100);
        currencyValues.Add(1000);
        currencyValues.Add(300);
        ChangeValues();
    }

    public void ChangeValues()
    {
        // ������� �� ��� ���� � canvas
        foreach (Transform Pref in Canvas)
        {
            Destroy(Pref.gameObject);
        }

        int count = 0;
        foreach (string currencyName in currencyNames)
        {
            textCurrencyNames.text = currencyName;
            textcurrencyValues.text = $"{currencyValues[count]}";
            CopyPref(Pref, Canvas);
            count++;
        }
    }

    private GameObject CopyPref(GameObject Prefub, Transform setparent)
    {
        var spawn = Instantiate(Prefub);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;

    }
}
