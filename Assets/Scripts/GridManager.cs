using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private Transform gridCanvas;

    private Transform gridParent { get { return gridCanvas.GetChild(0); } }

    private GridCell[] gridCells;

    public int indexPerRow = 1,
               indexPerColumn = 10;

    public System.Action OnCellsDestroyed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gridCells = new GridCell[gridParent.childCount];

        for (var n = 0; n < gridParent.childCount; n++)
        {
            GridCell cell = gridParent.GetChild(n).GetComponent<GridCell>();
            gridCells[n] = cell;

            cell.index = n;
        }
    }

    public bool IsCellEmpty(int index)
    {
        if (index < 0 || index >= gridCells.Length) return false;

        return !gridCells[index].isOccupied;
    }

    public GridCell GetCell(int index)
    {
        return gridCells[index];
    }

    public Vector2 GetGridCellPos(int targetCell)
    {
        return gridCells[targetCell].transform.position;
    }
}
