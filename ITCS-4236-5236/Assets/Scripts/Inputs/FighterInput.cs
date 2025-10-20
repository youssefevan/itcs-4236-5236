public interface IFighterInput
{
    float moveInput { get; }
    float aimInput { get; }
    bool jumpInput { get; }
    bool crouchInput { get; }
    bool blockInput { get; }
    bool punchInput { get; }
    bool kickInput { get; }
    bool dodgeInput { get; }
}
