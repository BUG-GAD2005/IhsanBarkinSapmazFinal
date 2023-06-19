using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeBlockPieceSelector : MonoBehaviour, IPointerDownHandler
{
    private ShapeBlockPiece blockPiece;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (blockPiece.ownerBlock != null)
        {
            PlayerController.Instance.SelectBlock(blockPiece.ownerBlock);
        }
    }

    private void Start()
    {
        blockPiece = GetComponent<ShapeBlockPiece>();
    }
}
