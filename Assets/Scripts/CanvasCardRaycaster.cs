using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCardRaycaster : MonoBehaviour
{
    private GraphicRaycaster raycaster;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();

        PlayerController.Instance.OnBlockSelected += DeactivateRaycaster;
        PlayerController.Instance.OnBlockUnselected += ActivateRaycaster;
    }

    private void ActivateRaycaster()
    {
        raycaster.enabled = true;
    }

    private void DeactivateRaycaster()
    {
        raycaster.enabled = false;
    }
}
