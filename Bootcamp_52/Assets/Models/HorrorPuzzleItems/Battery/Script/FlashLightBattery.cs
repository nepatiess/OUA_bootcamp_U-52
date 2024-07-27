using UnityEngine;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    public float maxBatteryLife = 100f; // Maksimum pil s�resi
    private float currentBatteryLife; // Mevcut pil s�resi
    public Text batteryLifeText; // Pil s�resi g�stergesi

    void Start()
    {
        currentBatteryLife = maxBatteryLife; // Ba�lang��ta pil s�resini maksimum yap
        UpdateBatteryLifeText();
        InvokeRepeating("DecreaseBatteryLife", 1f, 1f); // Pil s�resini saniyede bir azalt
    }

    void UpdateBatteryLifeText()
    {
        batteryLifeText.text = "Battery: " + Mathf.Clamp(currentBatteryLife, 0, maxBatteryLife).ToString("F0") + "%";
    }

    void DecreaseBatteryLife()
    {
        if (currentBatteryLife > 0)
        {
            currentBatteryLife -= 1; // Pil s�resini azalt
            UpdateBatteryLifeText();
        }
    }

    public void AddBatteryLife(float amount)
    {
        currentBatteryLife = Mathf.Clamp(currentBatteryLife + amount, 0, maxBatteryLife); // Pil s�resini art�r
        UpdateBatteryLifeText();
    }
}
