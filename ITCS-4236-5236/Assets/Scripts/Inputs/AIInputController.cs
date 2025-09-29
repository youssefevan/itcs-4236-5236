using UnityEngine;

public class AIInputController : MonoBehaviour, IFighterInput
{
    public float moveInput { get; private set; }
    public float aimInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool crouchInput { get; private set; }
    public bool blockInput { get; private set; }
    public bool punchInput { get; private set; }
    public bool kickInput { get; private set; }

    public void SetInputs(float move, float aim, bool jump, bool crouch, bool block, bool punch, bool kick)
    {
        moveInput = move;
        aimInput = aim;
        jumpInput = jump;
        crouchInput = crouch;
        blockInput = block;
        punchInput = punch;
        kickInput = kick;
    }

    public void Idle()
    {
        SetInputs(0, 0, false, false, false, false, false);
    }

    public void Move(float dir)
    {
        SetInputs(dir, 0, false, false, false, false, false);
    }
}
