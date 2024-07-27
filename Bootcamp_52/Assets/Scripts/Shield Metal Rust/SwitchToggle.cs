using UnityEngine;

public class SwitchToggle : MonoBehaviour
{
    public Vector3 offPosition; // OFF pozisyonu
    public Vector3 onPosition;  // ON pozisyonu
    private bool isOn = false;  // Tuþun durumu, baþlangýçta OFF (false)
    public AudioSource audioSource; // Ses kaynaðý

    void Start()
    {
        transform.localPosition = offPosition; // Tuþun baþlangýç pozisyonunu ayarla
        audioSource = GetComponent<AudioSource>(); // AudioSource bileþenine referans al
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tuþ þu anda ON, OFF pozisyonuna geç
            transform.localPosition = offPosition;
        }
        else
        {
            // Tuþ þu anda OFF, ON pozisyonuna geç
            transform.localPosition = onPosition;
        }
        isOn = !isOn; // Durumu tersine çevir

        // Ses çal
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
