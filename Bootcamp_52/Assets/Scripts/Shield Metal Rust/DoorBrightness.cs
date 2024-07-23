using UnityEngine;

public class DoorBrightness : MonoBehaviour
{
    private Renderer objRenderer;
    private Material originalMaterial;
    public float highlightIntensity = 1.5f; // Vurgulama parlaklýk yoðunluðu

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;
    }

    void OnMouseEnter()
    {
        ChangeBrightness(highlightIntensity);
    }

    void OnMouseExit()
    {
        ChangeBrightness(1.0f); // Normal parlaklýk
    }

    void ChangeBrightness(float intensity)
    {
        // Objeye ait materyali kopyala ve parlaklýk ayarýný yap
        Material mat = new Material(originalMaterial);
        Color originalColor = originalMaterial.color;
        Color finalColor = originalColor * intensity;
        mat.color = finalColor;

        // Objeye yeni materyali ata
        objRenderer.material = mat;
    }
}
