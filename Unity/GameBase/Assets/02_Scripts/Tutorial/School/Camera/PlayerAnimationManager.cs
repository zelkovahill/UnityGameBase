using System.Collections;
using System.Collections.Generic;
using Tutorial.School.Camera;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    private const string PARAM_IS_MOVING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "Isjumping";
    private const string PARAM_IS_FALLING = "IsFalling";
    private const string PARAM_IS_ATTACK_TRIGGER = "Attack";

    public void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (stateMachine.currentState is not null)
        {
            ResetAllBoolParameters();

            switch (stateMachine.currentState)
            {
                case PlayerState.IdleState:

                    break;

                case PlayerState.MovingState:
                    animator.SetBool(PARAM_IS_MOVING, true);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;

                case PlayerState.JumpingState:
                    animator.SetBool(PARAM_IS_JUMPING, true);
                    break;

                case PlayerState.FallingState:
                    animator.SetBool(PARAM_IS_FALLING, true);
                    break;
            }
        }
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(PARAM_IS_ATTACK_TRIGGER);
    }

    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
        animator.SetBool(PARAM_IS_JUMPING, false);
        animator.SetBool(PARAM_IS_FALLING, false);
    }
}
