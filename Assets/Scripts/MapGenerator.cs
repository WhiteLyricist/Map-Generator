using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _cellPref; //������ ��������

    private int radius = 2; //���-�� ����� ��� ���������
    private int _startRadis;
    private int _count; //��������� ������ ������ ���������

    private List<Vector2> tileCoordinates = new List<Vector2>();

    private float _horizontalDisplacement = 0; //��� ������ ��������������� ���� ������ ���� ��������� �� �����������
    private float _verticalDisplacement = 0; //��� ������ ��������������� ������ ������ ���� ���������� �� ���������

    private void Start()
    {
        MoveCamera.EnlargeMap+=OnEnlargeMap;
    }

    public void ClearMap() 
    {
        _startRadis = Random.Radius;

        //������ ������ ���������
        tileCoordinates.Clear();

        _count = 0;
        radius = _startRadis;

        var ObjectCell = GameObject.FindGameObjectsWithTag("Cell");
        if (ObjectCell.Length > 0)
        {
            for (int i = 0; i < ObjectCell.Length; i++)
            {
                Destroy(ObjectCell[i]);
            }
        }

    }

    public void CellGenerator()
    {
        //��� ��� ���������� ������ ������������
        //��������� ������� ������
        tileCoordinates.Add(new Vector2(0, 0));

        //������� ����������� ������
        for (int i = 0; i < radius; i++)
        {
            tileCoordinates.Add(new Vector3(0, i + 1));
            tileCoordinates.Add(new Vector3(0, -i - 1));
        }
        //���������� ���������� ������
        int rowsRemaining = radius * 2; //����������� ���������� �����, ���������� ��� ���������
        _horizontalDisplacement = 0;
        _verticalDisplacement = 0;
        int currentRowLength = radius * 2; //����� ������� ����������� ������ (���������� ������)

        //���� ���� ����������� ���� ��� ��� ������ ���������� ������
        for (int rowID = 0; rowID < rowsRemaining; rowID++)
        {
            //���� �������� �������� ����� (����� ������� ������������ �� ������ ������), �������� ��������
            if (rowID == radius)
            {
                _horizontalDisplacement = 0;
                _verticalDisplacement = 0;
                currentRowLength = radius * 2;
            }

            //��� ������ ������ �������� ��������
            _horizontalDisplacement += 0.5f;
            currentRowLength -= 1;

            //���� ��� ������� ���
            if (rowID < radius)
            {
                _verticalDisplacement += 0.866f;
            }
            //���� ��� ������ ���
            else
            {
                _verticalDisplacement -= 0.866f;
            }

            //������������ ���������� ������ ��� ���� ������
            for (int tileID = 0; tileID <= currentRowLength; tileID++)
            {
                tileCoordinates.Add(new Vector2(_verticalDisplacement, radius - tileID - _horizontalDisplacement));
            }
        }

        CreatingTile();

    }

    private void CreatingTile() 
    {
        //����������� ��������������� ������ ��������� ��� �������� �������� ������
        for (int i = _count; i < tileCoordinates.Count; i++)
        {
            int typeTile = UnityEngine.Random.Range(0, 4);

            //�������� ����� ������ � �������� ��
            GameObject newTile = (GameObject)Instantiate(_cellPref[typeTile], tileCoordinates[i], Quaternion.identity);
            newTile.gameObject.name = "tile_" + i.ToString();
        }
        _count = tileCoordinates.Count;
    }

    private void AddCicle() 
    {
        _verticalDisplacement = radius;
        _horizontalDisplacement = 0;

        //���� ���� ����������� ���� ��� ��� ������ ���������� ������
        for (int rowID = 0; rowID < radius * 6; rowID++)
        {
            if (0 <= rowID && rowID < radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _horizontalDisplacement += 0.866f;
                _verticalDisplacement -= 0.5f;
            }
            if (radius <= rowID && rowID < 2 * radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _verticalDisplacement -= 1.0f;
            }
            if (2 * radius <= rowID && rowID < 3 * radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _verticalDisplacement -= 0.5f;
                _horizontalDisplacement -= 0.866f;
            }
            if (3 * radius <= rowID && rowID < 4 * radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _verticalDisplacement += 0.5f;
                _horizontalDisplacement -= 0.866f;
            }
            if (4 * radius <= rowID && rowID < 5 * radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _verticalDisplacement += 1.0f;
            }
            if (5 * radius <= rowID && rowID < 6 * radius)
            {
                tileCoordinates.Add(new Vector2(_horizontalDisplacement, _verticalDisplacement));
                _verticalDisplacement += 0.5f;
                _horizontalDisplacement += 0.866f;
            }
        }

        CreatingTile();

    }

    private void OnEnlargeMap() 
    {
        radius++;
        AddCicle();
    }

    private void OnDestroy()
    {
        MoveCamera.EnlargeMap -= OnEnlargeMap;
    }
}
