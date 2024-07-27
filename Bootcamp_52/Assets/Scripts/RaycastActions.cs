using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastActions : MonoBehaviour
{
    private Camera cam;
    private Ray ray;
    private RaycastHit hit;

    private Targetable currentTargetable;
    private Collectable currentCollectable;
    public Text pickupMessage; // PickupMessage text UI element

    private void Start()
    {
        cam = Camera.main;
        pickupMessage.text = ""; // Initialize the pickup message text to empty
    }

    private void Update()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.TryGetComponent(out Targetable targetable))
            {
                if (currentTargetable != null && currentTargetable != targetable)
                {
                    currentTargetable.ToggleHýghLight(false);
                }

                currentTargetable = targetable;
                currentTargetable.ToggleHýghLight(true);

                if (hit.collider.TryGetComponent(out Collectable collectable))
                {
                    currentCollectable = collectable;
                }
                else
                {
                    currentCollectable = null;
                }
            }
            else
            {
                if (currentTargetable != null)
                {
                    currentTargetable.ToggleHýghLight(false);
                    currentTargetable = null;
                }

                currentCollectable = null;
            }
        }
        else
        {
            if (currentTargetable != null)
            {
                currentTargetable.ToggleHýghLight(false);
                currentTargetable = null;
            }

            currentCollectable = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentCollectable)
            {
                pickupMessage.text = currentCollectable.name + " added to inventory.";
                StartCoroutine(ClearPickupMessageAfterDelay(5)); // Clear message after 5 seconds
                currentCollectable.Collect();
                currentCollectable = null;
            }
        }
    }
    private IEnumerator ClearPickupMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickupMessage.text = "";
    }
}
