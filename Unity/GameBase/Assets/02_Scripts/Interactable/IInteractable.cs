using UnityEngine;


namespace Interactable
{
    // 모든 상호 작용 가능한 객체가 구현해야하는 기본 인터페이스
    public interface IInteractable
    {
        /// <summary>
        /// 상호작용 시 표시할 텍스트
        /// </summary>
        /// <returns></returns>
        string GetInteractPrompt();


        /// <summary>
        /// 상호작용 시 실행될 메서드
        /// </summary>
        /// <param name="player"></param>
        void OnInteract(GameObject player);


        /// <summary>
        /// 상호작용 가능 거리
        /// </summary>
        /// <returns></returns>
        float GetInteractionDistance();


        /// <summary>
        /// 상호작용 가능 여부
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        bool CanInteract(GameObject player);
    }
}