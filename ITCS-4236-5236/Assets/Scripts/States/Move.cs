using UnityEngine;

public class Move : State
{

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        fighter.FaceOpponent();

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, fighter.inputType.moveInput * fighter.maxSpeed,
            fighter.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        // reverse anim when moving away from opponent
        bool isMovingForward = (fighter.rb.linearVelocityX * fighter.facing) > 0;
        bool isMovingBackward = (fighter.rb.linearVelocityX * fighter.facing) < 0;

        AnimatorClipInfo[] info = fighter.animator.GetCurrentAnimatorClipInfo(0);
        string currentAnim = info[0].clip.name;

        if (isMovingForward && currentAnim != "move")
        {
            fighter.animator.Play("move");
        }
        else if (isMovingBackward && currentAnim != "retreat")
        {
            fighter.animator.Play("retreat");
        }



        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (fighter.inputType.moveInput == 0f)
        {
            return states["idle"];
        }

        if (fighter.inputType.jumpInput)
        {
            return states["jumpsquat"];
        }

        if (fighter.inputType.blockInput)
        {
            return states["block"];
        }

        if (fighter.inputType.crouchInput)
        {
            return states["crouch"];
        }

        if (fighter.inputType.kickInput)
        {
            return states["kickHigh"];
        }

        if (fighter.inputType.punchInput)
        {
            return states["punchHigh"];
        }

        return null;
    }

    public override void Exit()
    {
        base.Exit();

        // needed for fighter 3 so i dont have to do it in every animation
        fighter.sprite.flipX = false;
    }
}