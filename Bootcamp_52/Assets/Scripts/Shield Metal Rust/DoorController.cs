using UnityEngine;
using System.Collections; // IEnumerator i�in gerekli ad alan�

public class DoorController : MonoBehaviour
{
    public Vector3 closedRotation = new Vector3(-450f, 0f, 90f); // Kapal� pozisyon rotasyonu
    public Vector3 openRotation = new Vector3(-275f, 0f, 90f);   // A��k pozisyon rotasyonu
    public float openCloseSpeed = 50f;  // A��l�� ve kapan�� h�z�
    public AudioSource audioSource;     // Ses kayna��
    private bool isOpen = false;        // Kap�n�n durumu, ba�lang��ta kapal�

    void Start()
    {
        // Kap�n�n ba�lang�� pozisyonunu kapal� olarak ayarla
        transform.localRotation = Quaternion.Euler(closedRotation);
    }

    void OnMouseDown()
    {
        // Kap�y� a�ma veya kapama i�lemleri
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        // A��k pozisyon rotasyonunu hedef alarak kap�y� a�
        Quaternion targetRotation = Quaternion.Euler(openRotation);
        StartCoroutine(RotateDoor(targetRotation)); // Coroutine ba�lat
        isOpen = true;

        // Ses �al
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void CloseDoor()
    {
        // Kapal� pozisyon rotasyonunu hedef alarak kap�y� kapat
        Quaternion targetRotation = Quaternion.Euler(closedRotation);
        StartCoroutine(RotateDoor(targetRotation)); // Coroutine ba�lat
        isOpen = false;

        // Ses �al
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        // Kap�y� hedef rotasyona do�ru d�nd�r
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openCloseSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
    }
}
