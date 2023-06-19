using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public int HoveredCell { get; private set; }
    public ShapeBlock SelectedBlock { get; private set; }

    public System.Action OnBlockSelected,
                         OnBlockUnselected;

    private void Awake()
    {
        Instance = this;

        HoveredCell = -1;
    }

    public void HoverCell(int index)
    {
        if (HoveredCell != -1) UnhoverCell();

        HoveredCell = index;

        GridManager.Instance.GetCell(HoveredCell).Hover();
    }

    public void UnhoverCell()
    {
        if (HoveredCell != -1)
        {
            GridManager.Instance.GetCell(HoveredCell).Unhover();
        }

        HoveredCell = -1;
    }

    public void SelectBlock(ShapeBlock block)
    {
        UnselectBlock();

        SelectedBlock = block;
        SelectedBlock.selector.SelectBlock();

        OnBlockSelected?.Invoke();
    }

    private void UnselectBlock()
    {
        if (SelectedBlock != null)
        {
            SelectedBlock.selector.DropBlock();

            SelectedBlock = null;

            OnBlockUnselected?.Invoke();
        }
    }

    private void PlaceBlock()
    {
        SelectedBlock.PlaceBlock();

        SelectedBlock = null;

        OnBlockUnselected?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && SelectedBlock != null)
        {
            if (HoveredCell > -1 && SelectedBlock.CanPlace())
            {
                PlaceBlock();
            }
            else
            {
                UnselectBlock();
            }
        }
    }
}
