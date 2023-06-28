using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShapeBlockSelector : MonoBehaviour
{
    private ShapeBlock block;

    private Vector3 defaultPosition;

    private bool isSelected;

    RaycastHit2D rayHitToCheckIsOccupiedOrNot;
    private void Start()
    {
        block = GetComponent<ShapeBlock>();

        defaultPosition = transform.position;
    }

    private void Update()
    {
        if (isSelected)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void SelectBlock()
    {
        isSelected = true;
    }

    public void DropBlock()
    {
        isSelected = false;

        //transform.position = defaultPosition;

        Destroy(this.gameObject);
    }
}
