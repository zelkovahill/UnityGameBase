using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField]
    private Health health;

    [Header("View")]
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Text HealthLabel;

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

        if (health.MaxHealth is not 0)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateDliderSmmothly());
        }
        // if (health.MaxHealth != 0)
        // {
        //     healthSlider.value = ((float)health.CurrentHealth / (float)health.MaxHealth) * 100f;
        // }

        // if (HealthLabel != null)
        // {
        //     HealthLabel.text = health.CurrentHealth.ToString();
        // }
    }

    public void Health_HealthChanged()
    {
        UpdateView();
    }

    private IEnumerator UpdateDliderSmmothly()
    {
        float targetValue = ((float)health.CurrentHealth / (float)health.MaxHealth) * 100f;
        float initialValue = healthSlider.value;

        // 텍스트에서 숫자를 안전하게 파싱
        float initialLabelValue = 0f;
        if (!float.TryParse(HealthLabel.text, out initialLabelValue))
        {
            initialLabelValue = health.CurrentHealth; // 기본값으로 설정
        }

        float duration = 0.5f; // 애니메이션 지속 시간
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            healthSlider.value = Mathf.Lerp(initialValue, targetValue, t);

            HealthLabel.text = Mathf
                .Lerp(initialLabelValue, health.CurrentHealth, t)
                .ToString("F0");

            yield return null;
        }

        // 최종값 설정
        healthSlider.value = targetValue;
        HealthLabel.text = health.CurrentHealth.ToString();
    }
}
