using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 direction = mainCameraTransform.position - transform.position;
        direction.y = 0; // Optional: Keep the text upright
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}
