using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tutorial.School.Camera
{
    public class FloatingTextManager : MonoBehaviour
    {
        public static FloatingTextManager instance;
        public GameObject textPrefabs;

        private void Awake()
        {
            instance = this;
        }

        public void Show(string text, Vector3 worldPos)
        {
            Vector2 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(worldPos);

            GameObject textObj = Instantiate(textPrefabs, transform);
            textObj.transform.position = screenPos;

            TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

            if (temp is not null)
            {
                temp.text = text;

                StartCoroutine(AnimateText(textObj));
            }
        }

        private IEnumerator AnimateText(GameObject textObj)
        {
            float duration = 1f;
            float timer = 0;

            Vector3 startPos = textObj.transform.position;
            TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float progress = timer / duration;

                textObj.transform.position = startPos + Vector3.up * (progress * 50f);

                if (temp is not null)
                {
                    temp.alpha = 1 - progress;
                }
                yield return null;
            }

            Destroy(textObj);
        }
    }
}