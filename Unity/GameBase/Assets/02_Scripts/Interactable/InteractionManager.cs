using UnityEngine;
using TMPro;

namespace Interactable
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance { get; private set; }

        [Header("UI 레퍼런스")]

        [SerializeField]
        [Tooltip("프롬프트 텍스트")]
        private TextMeshProUGUI promptText;

        [SerializeField]
        [Tooltip("프롬프트 텍스트가 표시될 위치")]
        private float checkRadius = 3f;

        [SerializeField]
        [Tooltip("상호작용 가능한 레이어")]
        private LayerMask interactableLayers;

        private IInteractable currentInteractable;  // 현재 상호작용 가능한 객체
        private GameObject player;                  // 플레이어 객체 

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

            // 플레이어 객체를 찾는다.
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            // 상호작용 가능한 객체를 찾아 상호작용을 시도한다.
            CheckInteractables();

            // 상호작용 버튼을 누르면 상호작용을 시도한다.
            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
            {
                currentInteractable.OnInteract(player);
            }
        }


        /// <summary>
        /// 상호작용 가능한 객체를 찾아 반환하는 메서드
        /// </summary>
        private void UpdatePrompt()
        {
            if (currentInteractable != null)
            {
                promptText.text = $"[E] {currentInteractable.GetInteractPrompt()}";
                promptText.gameObject.SetActive(true);
            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// 주변 상호작용 가능한 객체를 찾는 메서드
        /// </summary>
        private void CheckInteractables()
        {
            // 주변 상호작용 가능한 객체를 찾는다.
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, checkRadius, interactableLayers);

            IInteractable closest = null;           // 가장 가까운 상호작용 가능한 객체
            float closestDistance = float.MaxValue; // 가장 가까운 상호작용 가능한 객체와의 거리

            // 주변 상호작용 가능한 객체 중 가장 가까운 객체를 찾는다.
            foreach (var col in colliders)
            {
                // 상호작용 가능한 객체인지 확인한다.
                if (col.TryGetComponent<IInteractable>(out var interactable))
                {
                    // 플레이어와 상호작용 가능한 객체 사이의 거리를 계산한다.
                    float distance = Vector3.Distance(player.transform.position, col.transform.position);

                    // 상호작용 가능한 객체 중 가장 가까운 객체를 찾는다.
                    if (distance <= interactable.GetInteractionDistance() && distance < closestDistance && interactable.CanInteract(player))
                    {
                        closest = interactable;
                        closestDistance = distance;
                    }
                }
            }

            // 가장 가까운 상호작용 대상 업데이트
            currentInteractable = closest;
            UpdatePrompt();
        }


    }
}