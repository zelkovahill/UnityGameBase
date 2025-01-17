using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action HealthChanged;

    private const int MIN_HEALTH = 0;
    private const int MAX_HEALTH = 100;
    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, MIN_HEALTH, MAX_HEALTH);
            HealthChanged?.Invoke();
        }
    }

    public int MinHealth => MIN_HEALTH;
    public int MaxHealth => MAX_HEALTH;

    public void Increment(int amount)
    {
        CurrentHealth += amount;
    }

    public void Decrement(int amount)
    {
        CurrentHealth -= amount;
    }

    public void Restore()
    {
        CurrentHealth = MAX_HEALTH;
    }
}
