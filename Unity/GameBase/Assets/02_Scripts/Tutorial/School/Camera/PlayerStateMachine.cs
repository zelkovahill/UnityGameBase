using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static Tutorial.School.Camera.PlayerState;

namespace Tutorial.School.Camera
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerState currentState;
        public SimplePlayerController playerController;

        private void Awake()
        {
            playerController = GetComponent<SimplePlayerController>();
        }

        private void Start()
        {
            TransitionToState(new PlayerState.IdleState(this));
        }

        private void Update()
        {
            if (currentState is not null)
            {
                currentState.Update();
            }
        }

        private void FixedUpdate()
        {
            if (currentState is not null)
            {
                currentState.FixedUpdate();
            }
        }

        public void TransitionToState(PlayerState newState)
        {
            if (currentState?.GetType() == newState.GetType())
            {
                return;
            }

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
            Debug.Log($"Transtioned to State {newState.GetType().Name}");
        }
    }
}