using UnityEngine;

public class electricInteractable : MonoBehaviour

{
    public void electricInteract()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        // Etkileþim iþlemleri burada yapýlýr
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
