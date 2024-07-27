using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    Transform cam;
    [SerializeField] LayerMask itemLayer;

    private Image currentHoverImage; // Current hover image being shown
    private ItemObject currentItem; // Currently interacted item

    void Start()
    {
        cam = Camera.main.transform;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    void Update()
    {
        if (cam == null) return;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 2, itemLayer))
        {
            ItemObject itemObject = hit.collider.GetComponent<ItemObject>();
            if (itemObject == null) return;

            if (currentItem != itemObject)
            {
                // Disable the current hover image if switching items
                if (currentHoverImage != null)
                {
                    currentHoverImage.enabled = false;
                }

                currentItem = itemObject;
                currentHoverImage = currentItem.GetCurrentHoverImage();
                if (currentHoverImage != null)
                {
                    currentHoverImage.enabled = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Handle item pickup logic here
                Debug.Log($"Picked up {itemObject.amount}x {itemObject.itemStats.name}");
                // Implement your item pickup logic here
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Cycle to the next image on mouse click
                currentItem.NextImage();
                currentHoverImage = currentItem.GetCurrentHoverImage();
            }
        }
        else
        {
            // Disable the current hover image when not looking at an item
            if (currentHoverImage != null)
            {
                currentHoverImage.enabled = false;
                currentHoverImage = null;
            }
            currentItem = null;
        }
    }
}
