using UnityEngine;
using UnityEngine.UI;

public class PaperInteraction : MonoBehaviour
{
    public float raycastDistance = 5f; // Raycast mesafesi
    public LayerMask paperLayer; // Kaðýtlarýn bulunduðu katman
    public GameObject readPanel; // Okuma paneli
    private bool isReading = false;

    void Update()
    {
        if (!isReading)
        {
            // Raycast sadece panel açýk deðilken yapýlýr
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance, paperLayer))
            {
                Debug.Log("Raycast hit something within the paper layer.");
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                Debug.Log("Hit object layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

                Paper paper = hit.collider.gameObject.GetComponent<Paper>();
                if (paper != null)
                {
                    Debug.Log("Raycast hit a paper object.");

                    // E tuþuna basýldýðýnda
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("E key pressed.");
                        ToggleReadPanel();
                    }
                }
                else
                {
                    Debug.Log("Hit object does not have a Paper component.");
                }
            }
        }
        else
        {
            // Panel açýksa sadece E tuþuna basýlýp basýlmadýðýný kontrol ederiz
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed while reading.");
                ToggleReadPanel();
            }
        }
    }

    void ToggleReadPanel()
    {
        isReading = !isReading;
        readPanel.SetActive(isReading);
        Debug.Log("Read panel toggled. Is reading: " + isReading);
    }
}
