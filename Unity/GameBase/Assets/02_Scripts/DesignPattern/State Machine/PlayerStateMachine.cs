using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    [Serializable]
    public class PlayerStateMachine
    {
        public IState CurrentState { get; private set; }

        public WalkState walkState;
        public JumpState jumpState;
        public IdleState idleState;

        public event Action<IState> stateChanged;

        public PlayerStateMachine(PlayerController player)
        {
            this.walkState = new WalkState(player);
            this.jumpState = new JumpState(player);
            this.idleState = new IdleState(player);
        }

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            stateChanged?.Invoke(CurrentState);
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();

            stateChanged?.Invoke(CurrentState);
        }

        public void Execute()
        {
            if (CurrentState is not null)
            {
                CurrentState.Execute();
            }
        }

    }
}
