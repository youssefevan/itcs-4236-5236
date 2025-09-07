using UnityEngine;
public interface IFighterInput
{
    float moveInput { get; }
    Vector2 aimInput { get; }
    bool jumpInput { get; }
    bool crouchInput { get; }
    bool blockInput { get; }
    bool punchInput { get; }
    bool kickInput { get; }
}
