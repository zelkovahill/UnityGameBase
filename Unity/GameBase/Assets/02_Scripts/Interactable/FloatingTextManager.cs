using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Interactable
{
    public class FloatingTextManager : MonoBehaviour
    {
        private static FloatingTextManager instance;

        public static FloatingTextManager Instance => instance;
        public GameObject floatingTextPrefab;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// 텍스트를 플레이어 위치에서 떠다주게 한다.
        /// </summary>
        /// <param name="text">텍스트</param>
        /// <param name="position">생성 위치</param>
        public void ShowFloatingText(string text, Vector3 position)
        {
            var go = Instantiate(floatingTextPrefab, position + Vector3.up * 2f, Quaternion.identity);
            go.GetComponent<TextMeshPro>().SetText(text);
            Destroy(go, 2f);
        }
    }
}