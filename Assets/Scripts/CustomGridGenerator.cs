using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGridGenerator : MonoBehaviour
{
    [SerializeField] private int gridAmount = 10;
    [SerializeField] private float cellSize = 60f, cellOffset = 1f;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gridCanvas;

    public void GenerateGrid()
    {
        foreach (Transform grid in gridCanvas)
        {
            DestroyImmediate(grid.gameObject, false);
        }

        Transform gridParent = new GameObject("Grid").transform;
        gridParent.SetParent(gridCanvas);

        //Creates 10 * 10 grid
        for (int column = 0; column < gridAmount; column++)
        {
            for (int row = 0; row < gridAmount; row++)
            {
                Vector2 cellPos = GetCellPos(column, row, cellSize, cellOffset);

                cellPos = cellPos - (Vector2.one * cellSize * (gridAmount - 1) / 2) - (Vector2.one * cellOffset * (gridAmount - 1) / 2);

                RectTransform cell = Instantiate(cellPrefab, gridParent).GetComponent<RectTransform>();

                cell.anchoredPosition = cellPos;
                cell.sizeDelta = Vector2.one * cellSize;    
            }
        }
    }

    public static Vector2 GetCellPos(int column, int row, float cellSize, float cellOffSet)
    {
        float cellPosX = cellSize * column + cellOffSet * column;
        float cellPosY = cellSize*row + cellOffSet * row;

        Vector2 cellPos = new Vector2(cellPosX, cellPosY);

        return cellPos;
    }


}
