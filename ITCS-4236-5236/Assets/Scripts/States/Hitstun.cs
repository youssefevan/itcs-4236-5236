using UnityEngine;

public class Hitstun : State
{
    float currentTime = 0;
    float currentFrame = 0;
    float stunTime;

    Vector2 startingPos;
    float maxShake = 0.3f;

    public override void Enter()
    {
        base.Enter();
        fighter.hurtSFX.pitch = Random.Range(0.85f, 1.0f);
        fighter.hurtSFX.Play();

        stunTime = fighter.incomingStun;
        currentTime = 0;
        currentFrame = 0;

        startingPos = fighter.transform.position;
        fighter.sprite.color = Color.red;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = Vector2.zero;
        fighter.rb.linearVelocity = velocity;


        // Shake
        float shakeX = Random.Range(-maxShake, maxShake);
        float shakeY = Random.Range(-maxShake, maxShake);
        fighter.transform.position = startingPos + new Vector2(shakeX, shakeY);

        maxShake /= currentFrame+1;

        // exit
        if (currentTime >= stunTime)
        {
            return states["knockback"];
        }

        return null;
    }

    public override void Exit() {
        base.Exit();
        fighter.sprite.color = Color.white;
        fighter.transform.position = startingPos;
    }
}
