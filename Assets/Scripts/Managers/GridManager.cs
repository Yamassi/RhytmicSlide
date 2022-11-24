using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int _rows;
    [SerializeField] int _columns;
    [SerializeField] Vector3 _leftBottomLocation;
    [SerializeField] GameObject _gridPointPrefab;
    [SerializeField] GameObject _gridHolder;
    [SerializeField] PointController[,] _gridArray;
    int scale = 1;
    public static int[] GridSize = new int[2];
    public static PointController[,] GridArray;

    public void Init()
    {
        if (_gridPointPrefab != null)
        {
            GenerateGrid();
        }
        GridArray = _gridArray;
        GridSize[0] = _rows;
        GridSize[1] = _columns;
    }

    void GenerateGrid()
    {
        _gridArray = new PointController[_columns, _rows];
        for (int x = 0; x < _columns; x++)
        {
            for (int z = 0; z < _rows; z++)
            {
                GameObject obj = Instantiate(_gridPointPrefab, new Vector3(_leftBottomLocation.x + scale * x, _leftBottomLocation.y, _leftBottomLocation.z + scale * z), Quaternion.identity);
                obj.transform.SetParent(_gridHolder.transform);
                _gridArray[x, z] = obj.GetComponent<PointController>();
                ShaderManager.AddToColorBlinkShadersList(obj.GetComponent<ColorBlinkShaderController>());
            }
        }
    }
}
