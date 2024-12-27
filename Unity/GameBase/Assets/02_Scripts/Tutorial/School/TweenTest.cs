using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    public bool isPunch = false;

    private Vector3 orginalScale;
    private Renderer objectRenderer;

    private void Start()
    {
        orginalScale = transform.localScale;
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPunch)
            {
                isPunch = true;
                StartCoroutine(PunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f));
                StartCoroutine(ChangeColor(0.1f));

            }
        }
    }

    private IEnumerator PunchScale(Vector3 punchAmount, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float scaleFactor = Mathf.Sin((elapsedTime / duration) * Mathf.PI) * punchAmount.magnitude;
            transform.localScale = orginalScale + punchAmount.normalized * scaleFactor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = orginalScale;
        isPunch = false;
    }

    private IEnumerator ChangeColor(float duration)
    {
        Color originalColor = objectRenderer.material.color;
        Color targetColor = new Color(Random.value, Random.value, Random.value);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectRenderer.material.color = Color.Lerp(originalColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // 경과 시간 증가
            yield return null;
        }

        objectRenderer.material.color = targetColor;
    }
}
