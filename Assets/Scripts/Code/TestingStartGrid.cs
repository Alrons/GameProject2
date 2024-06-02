using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingStartGrid : MonoBehaviour
{
    private Grid grid;
    public GameObject MainCamera;
    private float mouseMoveTimer;
    private float mouseMoveTimerMax = .01f;
    public GameObject plasePrefub;
    public Transform plase;

    private void Start()
    {
        
    }

    // ������, ������, ������ ������, ������� � ��������, ��� �������, ������ ������, ������ ������ ������
    private void CreateTableWithDrag(int width, int height, float cellSize, int degrees, Vector3 SpawnPoint, GameObject plasePrefub, Transform plase)
    {
        GameObject OurBackTable = CopyPrefForTables(plase.gameObject, SpawnPoint, MainCamera.transform);

        OurBackTable.transform.localScale = new Vector3(width * cellSize * 2, height * cellSize);

        Vector3 position = OurBackTable.transform.position;
        Vector3 scale = OurBackTable.transform.localScale;
        Quaternion rotation = OurBackTable.transform.rotation;

        // ���������� ������ ����� ���� ������� � ��������� �����������
        Vector3 bottomLeftLocal = new Vector3(-0.5f * scale.x, -0.5f * scale.y, -0.5f * scale.z);

        Vector3 bottomLeftWorld = rotation * bottomLeftLocal;

        // ������� ������� �������
        bottomLeftWorld += position;

        grid = new Grid(width, height, cellSize, bottomLeftWorld, plasePrefub, OurBackTable.transform);
        // ������� � ��������
        OurBackTable.transform.Rotate(0, 0, degrees);
    }
    public GameObject CopyPrefForTables(GameObject box, Vector3 position, Transform setparent)
    {
        var spawn = UnityEngine.Object.Instantiate(box, position, Quaternion.identity);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.localScale = new Vector3(10f, (float)0.35, (float)0.35);

        return spawn;

    }
}
