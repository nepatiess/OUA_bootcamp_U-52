using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    public int amount;
    public ItemStats itemStats;
    public Image[] hoverImages; // Array of hover images

    private int currentImageIndex = 0;

    // Method to get the current hover image
    public Image GetCurrentHoverImage()
    {
        if (hoverImages.Length == 0) return null;
        return hoverImages[currentImageIndex];
    }

    // Method to cycle to the next hover image
    public void NextImage()
    {
        if (hoverImages.Length == 0) return;
        hoverImages[currentImageIndex].enabled = false; // Disable current image
        currentImageIndex = (currentImageIndex + 1) % hoverImages.Length; // Cycle to next image
        hoverImages[currentImageIndex].enabled = true; // Enable next image
    }
}

[System.Serializable]
public class ItemStats
{
    public string name;
    // Add other item stats if necessary
}
