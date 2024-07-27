using UnityEngine;

public class OutlineColor : MonoBehaviour
{
    private Renderer objRenderer;
    private Material outlineMaterial;
    private Material originalMaterial;

    public Color outlineColor = new Color(1.0f, 0.5f, 0.0f); // Turuncu renk

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;

        // Outline shader'�n� i�eren materyali y�kle
        outlineMaterial = new Material(Shader.Find("Outlined/Silhouetted Diffuse"));
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
    }

    void OnMouseEnter()
    {
        // Objeye outline materyalini ata
        objRenderer.material = outlineMaterial;
    }

    void OnMouseExit()
    {
        // Objeye orijinal materyali geri y�kle
        objRenderer.material = originalMaterial;
    }
}
