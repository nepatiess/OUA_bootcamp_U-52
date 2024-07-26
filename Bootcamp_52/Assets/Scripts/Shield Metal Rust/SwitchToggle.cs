using UnityEngine;

public class SwitchToggle : MonoBehaviour
{
    public Vector3 offPosition; // OFF pozisyonu
    public Vector3 onPosition;  // ON pozisyonu
    private bool isOn = false;  // Tu�un durumu, ba�lang��ta OFF (false)
    public AudioSource audioSource; // Ses kayna��

    void Start()
    {
        transform.localPosition = offPosition; // Tu�un ba�lang�� pozisyonunu ayarla
        audioSource = GetComponent<AudioSource>(); // AudioSource bile�enine referans al
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tu� �u anda ON, OFF pozisyonuna ge�
            transform.localPosition = offPosition;
        }
        else
        {
            // Tu� �u anda OFF, ON pozisyonuna ge�
            transform.localPosition = onPosition;
        }
        isOn = !isOn; // Durumu tersine �evir

        // Ses �al
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
