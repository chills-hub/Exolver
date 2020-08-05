using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;

    void Start()
    {
        Slider = transform.GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth) 
    {
        Slider.maxValue = maxHealth;
        Slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        Slider.value = health;
    }
}
