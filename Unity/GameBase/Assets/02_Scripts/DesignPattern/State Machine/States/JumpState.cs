using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace State
{
    public class JumpState : IState
    {
        private PlayerController _player;

        public JumpState(PlayerController player)
        {
            this._player = player;
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            if (_player.IsGrounded)
            {
                if (Mathf.Abs(_player.CharacterController.velocity.x) < 0.1f && Mathf.Abs(_player.CharacterController.velocity.z) < 0.1f)
                {
                    _player.PlayerStateMachine.TransitionTo(_player.PlayerStateMachine.idleState);
                }
                else
                {
                    _player.PlayerStateMachine.TransitionTo(_player.PlayerStateMachine.walkState);
                }
            }
        }

        public void Exit()
        {

        }
    }
}