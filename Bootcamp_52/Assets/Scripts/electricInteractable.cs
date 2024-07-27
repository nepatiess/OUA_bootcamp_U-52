using UnityEngine;

public class electricInteractable : MonoBehaviour

{
    public void electricInteract()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        // Etkile�im i�lemleri burada yap�l�r
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
