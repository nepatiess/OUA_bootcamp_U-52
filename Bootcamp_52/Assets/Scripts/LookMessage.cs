using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMessage : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer; // Raycast'in etkile�ime ge�ece�i katman
    [SerializeField] private GameObject infoText; // Info texti olan 3D Text nesnesi
    private RaycastHit hit; // Raycast sonucunu tutacak de�i�ken

    void Update()
    {
        // Fare pozisyonundan bir ray (���n) olu�tur
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
        {
            if (hit.collider.CompareTag("Model"))
            {
                // Info texti aktif hale getir
                infoText.SetActive(true);

                // Textin rotasyonunu ayarla, kamera do�rultusunda olacak �ekilde
                infoText.transform.LookAt(Camera.main.transform);

                // Metnin yerle�tirme y�n�n� d�zeltmek i�in d�nd�rme i�lemi
                infoText.transform.rotation = Quaternion.LookRotation(infoText.transform.position - Camera.main.transform.position);
            }
            else
            {
                // E�er bak�lan nesne "Model" etiketine sahip de�ilse
                infoText.SetActive(false);
            }
        }
        else
        {
            // E�er hi�bir nesneye �arp�lmam��sa
            infoText.SetActive(false);
        }
    }
}
