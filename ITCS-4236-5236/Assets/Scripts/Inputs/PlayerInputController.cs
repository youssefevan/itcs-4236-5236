using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, IFighterInput
{
    public float moveInput { get; private set; }
    public float aimInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool crouchInput { get; private set; }
    public bool blockInput { get; private set; }
    public bool punchInput { get; private set; }
    public bool kickInput { get; private set; }
    public bool dodgeInput { get; private set; }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
    }

    public void Block(InputAction.CallbackContext context)
    {
        blockInput = context.performed;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        crouchInput = context.performed;
    }

    public void Punch(InputAction.CallbackContext context)
    {
        punchInput = context.performed;
    }

    public void Kick(InputAction.CallbackContext context)
    {
        kickInput = context.performed;
    }

    public void Dodge(InputAction.CallbackContext context)
    {
        dodgeInput = context.performed;
    }

}
