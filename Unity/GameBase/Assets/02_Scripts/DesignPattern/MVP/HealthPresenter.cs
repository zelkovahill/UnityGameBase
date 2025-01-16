using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private Health health;

    [Header("View")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text HealthLabel;

    private void Start()
    {

        health.HealthChanged += Health_HealthChanged;
        InitializeSlider();

        Reset();
        UpdateView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Heal(10);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Reset();
        }
    }

    private void OnDestroy()
    {
        health.HealthChanged -= Health_HealthChanged;
    }

    private void InitializeSlider()
    {
        health.CurrentHealth = health.MaxHealth;
    }

    public void Damage(int amount)
    {
        health.Decrement(amount);
    }

    public void Heal(int amount)
    {
        health.Increment(amount);
    }

    public void Reset()
    {
        health.Restore();
    }

    public void UpdateView()
    {
        if (health == null)
        {
            return;
        }

        if (health.MaxHealth != 0)
        {
            healthSlider.value = ((float)health.CurrentHealth / (float)health.MaxHealth) * 100f;
        }

        if (HealthLabel != null)
        {
            HealthLabel.text = health.CurrentHealth.ToString();
        }

    }

    public void Health_HealthChanged()
    {
        UpdateView();
    }
}

