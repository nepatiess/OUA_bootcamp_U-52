using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int RayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private DoorController raycastObj;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        if(Physics.Raycast(transform.position,fwd ,out hit,RayLenght,mask))
        {

            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    raycastObj = hit.collider.gameObject.GetComponent<DoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = false;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastObj. PlayAnimation();
                }
            }
        }
        else
        {
            if (!isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce=false;
            }
        }
    }
    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }

        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }

    }

}
