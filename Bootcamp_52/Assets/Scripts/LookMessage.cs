using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMessage : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer; // Raycast'in etkileþime geçeceði katman
    [SerializeField] private GameObject infoText; // Info texti olan 3D Text nesnesi
    private RaycastHit hit; // Raycast sonucunu tutacak deðiþken

    void Update()
    {
        // Fare pozisyonundan bir ray (ýþýn) oluþtur
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
        {
            if (hit.collider.CompareTag("Model"))
            {
                // Info texti aktif hale getir
                infoText.SetActive(true);

                // Textin rotasyonunu ayarla, kamera doðrultusunda olacak þekilde
                infoText.transform.LookAt(Camera.main.transform);

                // Metnin yerleþtirme yönünü düzeltmek için döndürme iþlemi
                infoText.transform.rotation = Quaternion.LookRotation(infoText.transform.position - Camera.main.transform.position);
            }
            else
            {
                // Eðer bakýlan nesne "Model" etiketine sahip deðilse
                infoText.SetActive(false);
            }
        }
        else
        {
            // Eðer hiçbir nesneye çarpýlmamýþsa
            infoText.SetActive(false);
        }
    }
}
