using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteract : MonoBehaviour
{
    [SerializeField] private Image customImage;

    void Start()
    {
        if (customImage == null)
        {
            Debug.LogError("CustomImage is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (customImage != null)
            {
                customImage.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (customImage != null)
            {
                customImage.enabled = false;
            }
        }
    }

    void Update()
    {
        // If this script needs an Update method, ensure references are checked here as well
        if (customImage == null) return;
    }
}
