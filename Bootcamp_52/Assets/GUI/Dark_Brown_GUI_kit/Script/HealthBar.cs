using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private Image _healthBarFill;

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealth(float amount)
    {
        _currentHealth += amount * _maxHealth; // Sa�l�k art���n� ekleme veya azaltma
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth; // Sa�l�k maksimum sa�l��� ge�memeli
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0; // Sa�l�k 0'�n alt�na d��memeli
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth;
        _healthBarFill.fillAmount = targetFillAmount;
    }
}
