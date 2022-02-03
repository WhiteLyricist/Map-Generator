using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _cellPref; //Массив префабов

    private int radius = 2; //Кол-во строк для генерации
    private int _startRadis;
    private int _count; //Начальный размер списка координат

    private List<Vector2> tileCoordinates = new List<Vector2>();

    private float _horizontalDisplacement = 0; //Как далеко сгенерированный тайл должен быть перемещен по горизонтали
    private float _verticalDisplacement = 0; //Как далеко сгенерированная плитка должна быть перемещена по вертикали

    private void Start()
    {
        MoveCamera.EnlargeMap+=OnEnlargeMap;
    }

    public void ClearMap() 
    {
        _startRadis = Random.Radius;

        //Пустой список координат
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
        //Код для заполнения списка координатами
        //Добавляет среднюю плитку
        tileCoordinates.Add(new Vector2(0, 0));

        //Создает центральную строку
        for (int i = 0; i < radius; i++)
        {
            tileCoordinates.Add(new Vector3(0, i + 1));
            tileCoordinates.Add(new Vector3(0, -i - 1));
        }
        //Генерирует оставшиеся строки
        int rowsRemaining = radius * 2; //Отслеживает количество строк, оставшихся для генерации
        _horizontalDisplacement = 0;
        _verticalDisplacement = 0;
        int currentRowLength = radius * 2; //Длина текущей создаваемой строки (количество тайлов)

        //Этот цикл запускается один раз для каждой оставшейся строки
        for (int rowID = 0; rowID < rowsRemaining; rowID++)
        {
            //Если пройдена половина строк (таким образом переключаясь на нижние строки), сбросить счетчики
            if (rowID == radius)
            {
                _horizontalDisplacement = 0;
                _verticalDisplacement = 0;
                currentRowLength = radius * 2;
            }

            //Для каждой строки обновите счетчики
            _horizontalDisplacement += 0.5f;
            currentRowLength -= 1;

            //Если это верхний ряд
            if (rowID < radius)
            {
                _verticalDisplacement += 0.866f;
            }
            //Если это нижний ряд
            else
            {
                _verticalDisplacement -= 0.866f;
            }

            //Сгенерируйте координаты плитки для этой строки
            for (int tileID = 0; tileID <= currentRowLength; tileID++)
            {
                tileCoordinates.Add(new Vector2(_verticalDisplacement, radius - tileID - _horizontalDisplacement));
            }
        }

        CreatingTile();

    }

    private void CreatingTile() 
    {
        //Используйте сгенерированный список координат для создания префабов тайлов
        for (int i = _count; i < tileCoordinates.Count; i++)
        {
            int typeTile = UnityEngine.Random.Range(0, 4);

            //Создайте новую плитку и назовите ее
            GameObject newTile = (GameObject)Instantiate(_cellPref[typeTile], tileCoordinates[i], Quaternion.identity);
            newTile.gameObject.name = "tile_" + i.ToString();
        }
        _count = tileCoordinates.Count;
    }

    private void AddCicle() 
    {
        _verticalDisplacement = radius;
        _horizontalDisplacement = 0;

        //Этот цикл запускается один раз для каждой оставшейся строки
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
