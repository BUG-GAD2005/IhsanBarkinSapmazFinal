using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridCell : MonoBehaviour
{
    public int index;
    public bool isOccupied {  get; private set; }

    private Color defaultColor;
    [SerializeField] private Color hoveredColor;

    private Image image;
    [SerializeField] private GameObject thePieceOfBlockThatGridHave;
    void Start()
    {
        image = GetComponent<Image>();

        defaultColor = image.color;
    }

    public void Occupy(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }

}
