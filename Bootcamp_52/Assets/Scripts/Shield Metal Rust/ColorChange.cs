using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer objRenderer;
    private Color originalColor;
    public Color highlightColor = new Color(1.0f, 0.5f, 0.0f); // Turuncu renk

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
    }

    void OnMouseEnter()
    {
        objRenderer.material.color = highlightColor;
    }

    void OnMouseExit()
    {
        objRenderer.material.color = originalColor;
    }
}
