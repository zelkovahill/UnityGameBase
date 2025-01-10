using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class IdleState : IState
    {
        private PlayerController _player;

        public IdleState(PlayerController player)
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

            if (Math.Abs(_player.CharacterController.velocity.x) > 0.1f || Math.Abs(_player.CharacterController.velocity.z) > 0.1f)
            {
                _player.PlayerStateMachine.TransitionTo(_player.PlayerStateMachine.walkState);
            }
        }

        public void Exit()
        {

        }
    }
}