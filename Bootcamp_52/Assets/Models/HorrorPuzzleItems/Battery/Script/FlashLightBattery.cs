using UnityEngine;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    public float maxBatteryLife = 100f; // Maksimum pil süresi
    private float currentBatteryLife; // Mevcut pil süresi
    public Text batteryLifeText; // Pil süresi göstergesi

    void Start()
    {
        currentBatteryLife = maxBatteryLife; // Baþlangýçta pil süresini maksimum yap
        UpdateBatteryLifeText();
        InvokeRepeating("DecreaseBatteryLife", 1f, 1f); // Pil süresini saniyede bir azalt
    }

    void UpdateBatteryLifeText()
    {
        batteryLifeText.text = "Battery: " + Mathf.Clamp(currentBatteryLife, 0, maxBatteryLife).ToString("F0") + "%";
    }

    void DecreaseBatteryLife()
    {
        if (currentBatteryLife > 0)
        {
            currentBatteryLife -= 1; // Pil süresini azalt
            UpdateBatteryLifeText();
        }
    }

    public void AddBatteryLife(float amount)
    {
        currentBatteryLife = Mathf.Clamp(currentBatteryLife + amount, 0, maxBatteryLife); // Pil süresini artýr
        UpdateBatteryLifeText();
    }
}
