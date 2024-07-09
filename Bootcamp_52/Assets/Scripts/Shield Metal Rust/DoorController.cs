using UnityEngine;
using System.Collections; // IEnumerator için gerekli ad alaný

public class DoorController : MonoBehaviour
{
    public Vector3 closedRotation = new Vector3(-450f, 0f, 90f); // Kapalý pozisyon rotasyonu
    public Vector3 openRotation = new Vector3(-275f, 0f, 90f);   // Açýk pozisyon rotasyonu
    public float openCloseSpeed = 50f;  // Açýlýþ ve kapanýþ hýzý
    public AudioSource audioSource;     // Ses kaynaðý
    private bool isOpen = false;        // Kapýnýn durumu, baþlangýçta kapalý

    void Start()
    {
        // Kapýnýn baþlangýç pozisyonunu kapalý olarak ayarla
        transform.localRotation = Quaternion.Euler(closedRotation);
    }

    void OnMouseDown()
    {
        // Kapýyý açma veya kapama iþlemleri
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
        // Açýk pozisyon rotasyonunu hedef alarak kapýyý aç
        Quaternion targetRotation = Quaternion.Euler(openRotation);
        StartCoroutine(RotateDoor(targetRotation)); // Coroutine baþlat
        isOpen = true;

        // Ses çal
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void CloseDoor()
    {
        // Kapalý pozisyon rotasyonunu hedef alarak kapýyý kapat
        Quaternion targetRotation = Quaternion.Euler(closedRotation);
        StartCoroutine(RotateDoor(targetRotation)); // Coroutine baþlat
        isOpen = false;

        // Ses çal
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        // Kapýyý hedef rotasyona doðru döndür
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openCloseSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
    }
}
