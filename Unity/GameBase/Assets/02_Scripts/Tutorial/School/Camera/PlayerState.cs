using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.School.Camera;

namespace Tutorial.School.Camera
{
    public abstract class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected SimplePlayerController playerController;

        public PlayerState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            this.playerController = stateMachine.playerController;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        protected void CheckTransitions()
        {
            if (playerController.isGrounded())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    stateMachine.TransitionToState(new JumpingState(stateMachine));
                }
                else if (Input.GetAxis("Horizontal") is not 0 || Input.GetAxis("Vertical") is not 0)
                {
                    stateMachine.TransitionToState(new MovingState(stateMachine));
                }
                else
                {
                    stateMachine.TransitionToState(new IdleState(stateMachine));
                }
            }
            else
            {
                if (playerController.GetVerticalVelocity() < 0)
                {
                    stateMachine.TransitionToState(new JumpingState(stateMachine));
                }
                else
                {
                    stateMachine.TransitionToState(new FallingState(stateMachine));
                }
            }
        }

        public class IdleState : PlayerState
        {
            public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

            public override void Update()
            {
                CheckTransitions();
            }
        }

        public class MovingState : PlayerState
        {
            private bool isRunning;
            public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

            public override void Update()
            {
                isRunning = Input.GetKey(KeyCode.LeftShift);

                CheckTransitions();
            }

            public override void FixedUpdate()
            {
                playerController.HandleMovement();
            }
        }

        public class JumpingState : PlayerState
        {
            public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

            public override void Update()
            {
                CheckTransitions();
            }

            public override void Enter()
            {
                playerController.HandleMovement();
            }
        }

        public class FallingState : PlayerState
        {
            public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

            public override void Update()
            {
                CheckTransitions();
            }

            public override void FixedUpdate()
            {
                playerController.HandleMovement();
            }
        }
    }
}