using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ThirdPersonController : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;


#if ENABLE_INPUT_SYSTEM

    public void OnMove(InputValue value)
    {

    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {

        }
    }

    public void OnJump(InputValue value)
    {

    }

    public void OnSprint(InputValue value)
    {

    }
#endif



}
