using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
        this.GetComponent<Image>().color = new Color(0,1,0,0.6f);
    }
}
