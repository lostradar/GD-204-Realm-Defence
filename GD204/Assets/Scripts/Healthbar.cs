using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image _healthbarSprite;

    public void UpdateHealthBar(float health, float currentHealth)
    {
        {
            _healthbarSprite.fillAmount = currentHealth / health;
        }
    }
}

