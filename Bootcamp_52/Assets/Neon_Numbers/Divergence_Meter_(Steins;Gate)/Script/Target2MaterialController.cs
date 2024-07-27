using UnityEngine;
using System.Collections;
public class Target2MaterialController : MonoBehaviour
{
    public Material target2OnMaterial;  // Hedef obje ON durumu materyali
    public AudioSource audioSource;     // Ses kaynaðý
    public float delay = 1.0f;          // Gecikme süresi (saniye cinsinden)

    void OnEnable()
    {
        LeverController.OnMaterialChanged += HandleMaterialChanged;
    }

    void OnDisable()
    {
        LeverController.OnMaterialChanged -= HandleMaterialChanged;
    }

    private void HandleMaterialChanged(GameObject target, Material material)
    {
        if (target == gameObject && material == target2OnMaterial)
        {
            // target2Object materyali target2OnMaterial'e geçerse gecikmeli ses çal
            if (audioSource != null)
            {
                StartCoroutine(PlaySoundWithDelay(delay));
            }
        }
    }

    private IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
