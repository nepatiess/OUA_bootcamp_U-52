using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 3.0f;
    public LayerMask interactableLayer;
    public KeyCode interactionKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                Debug.Log("Interacted with: " + hit.collider.gameObject.name);
                // Etkile�im yap�lacak i�lev burada �a�r�labilir
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
