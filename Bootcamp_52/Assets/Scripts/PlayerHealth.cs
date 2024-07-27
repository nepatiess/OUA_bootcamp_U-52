using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    private HealthBar healthBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
    }

    public void IncreaseHealth(float amount)
    {
        healthBar.UpdateHealth(amount);
    }

    public void DecreaseHealth(float amount)
    {
        healthBar.UpdateHealth(-amount);
    }
}
