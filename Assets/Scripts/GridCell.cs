using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public bool isOccupied { get; private set; }

    private Color defaultColor;
    [SerializeField] private Color hoveredColor;

    private Image image;

    public GameObject thePieceOfBlockThatGridHave;

    private void Start()
    {
        image = GetComponent<Image>();

        defaultColor = image.color;
    }

    public void Occupy(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }

    public void Hover()
    {
        image.color = hoveredColor;
    }

    public void Unhover()
    {
        image.color = defaultColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.Instance.HoverCell(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController controller = PlayerController.Instance;

        if (controller.HoveredCell == index)
        {
            controller.UnhoverCell();
        }
    }


}
