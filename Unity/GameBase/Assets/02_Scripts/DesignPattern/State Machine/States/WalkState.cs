using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class WalkState : IState
    {
        private PlayerController _player;

        public WalkState(PlayerController player)
        {
            this._player = player;
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            if (!_player.IsGrounded)
            {
                _player.PlayerStateMachine.TransitionTo(_player.PlayerStateMachine.jumpState);
            }

            if (Mathf.Abs(_player.CharacterController.velocity.x) < 0.1f && Mathf.Abs(_player.CharacterController.velocity.z) < 0.1f)
            {
                _player.PlayerStateMachine.TransitionTo(_player.PlayerStateMachine.idleState);
            }

        }

        public void Exit()
        {

        }
    }
}