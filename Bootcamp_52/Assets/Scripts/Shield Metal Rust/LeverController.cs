using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Vector3 offRotation; // OFF rotasyonu
    public Vector3 onRotation;  // ON rotasyonu
    public GameObject sphere; // K�re objesi
    private bool isOn = false;  // Tu�un durumu, ba�lang��ta OFF (false)
    public float transitionSpeed = 1.0f; // Ge�i� h�z�
    public AudioSource audioSource; // Ses kayna��
    private Quaternion targetRotation; // Hedef rotasyon
    private bool isTransitioning = false; // Ge�i�in devam edip etmedi�i

    void Start()
    {
        transform.localRotation = Quaternion.Euler(offRotation); // Tu�un ba�lang�� rotasyonunu ayarla
        targetRotation = Quaternion.Euler(offRotation);
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tu� �u anda ON, OFF rotasyonuna ge�
            targetRotation = Quaternion.Euler(offRotation);
            ChangeSphereColor(Color.white); // K�re rengini beyaza d�n��t�r
        }
        else
        {
            // Tu� �u anda OFF, ON rotasyonuna ge�
            targetRotation = Quaternion.Euler(onRotation);
            ChangeSphereColor(Color.green); // K�re rengini ye�ile d�n��t�r
        }
        isOn = !isOn; // Durumu tersine �evir
        isTransitioning = true; // Ge�i�in ba�lad���n� belirt

        // Ses �al
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isTransitioning)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * transitionSpeed);

            // Hedef rotasyona �ok yak�n olup olmad���n� kontrol et
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
                isTransitioning = false; // Ge�i�i durdur
            }
        }
    }

    void ChangeSphereColor(Color color)
    {
        if (sphere != null)
        {
            Renderer renderer = sphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color; // K�re rengini belirtilen renge ayarla
            }
        }
    }
}
