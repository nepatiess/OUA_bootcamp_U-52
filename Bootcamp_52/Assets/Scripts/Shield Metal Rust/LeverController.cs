using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Vector3 offRotation; // OFF rotasyonu
    public Vector3 onRotation;  // ON rotasyonu
    public GameObject sphere; // Küre objesi
    private bool isOn = false;  // Tuþun durumu, baþlangýçta OFF (false)
    public float transitionSpeed = 1.0f; // Geçiþ hýzý
    public AudioSource audioSource; // Ses kaynaðý
    private Quaternion targetRotation; // Hedef rotasyon
    private bool isTransitioning = false; // Geçiþin devam edip etmediði

    void Start()
    {
        transform.localRotation = Quaternion.Euler(offRotation); // Tuþun baþlangýç rotasyonunu ayarla
        targetRotation = Quaternion.Euler(offRotation);
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            // Tuþ þu anda ON, OFF rotasyonuna geç
            targetRotation = Quaternion.Euler(offRotation);
            ChangeSphereColor(Color.white); // Küre rengini beyaza dönüþtür
        }
        else
        {
            // Tuþ þu anda OFF, ON rotasyonuna geç
            targetRotation = Quaternion.Euler(onRotation);
            ChangeSphereColor(Color.green); // Küre rengini yeþile dönüþtür
        }
        isOn = !isOn; // Durumu tersine çevir
        isTransitioning = true; // Geçiþin baþladýðýný belirt

        // Ses çal
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

            // Hedef rotasyona çok yakýn olup olmadýðýný kontrol et
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation; // Rotasyonu tam olarak hedef rotasyona ayarla
                isTransitioning = false; // Geçiþi durdur
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
                renderer.material.color = color; // Küre rengini belirtilen renge ayarla
            }
        }
    }
}
