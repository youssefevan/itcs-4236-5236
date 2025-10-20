using System;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AIAction")]
public class AIAction : ScriptableObject
{
    public AnimationCurve oppDist;
    public AnimationCurve attackRemaining;
    protected List<float> scores = new List<float>();

    public float CalculateScore(Fighter f, AIInputController controller)
    {
        scores.Clear();
        EvaluateContext(f, controller);

        float finalScore = 1f;
        foreach (var score in scores)
        {
            finalScore *= score;
        }

        finalScore *= Random.Range(0.5f, 1.5f);

        Debug.Log(name + ": " + finalScore);
        return finalScore;
    }

    public virtual void EvaluateContext(Fighter f, AIInputController controller)
    {
        if (
            f.stateManager.GetCurrentState() is Attack ||
            f.stateManager.GetCurrentState() is Jumpsquat ||
            f.stateManager.GetCurrentState() is Land ||
            f.stateManager.GetCurrentState() is Hitstun ||
            f.stateManager.GetCurrentState() is Knockback ||
            f.stateManager.GetCurrentState() is Dodge
        )
        {
            scores.Add(0f);
            controller.Idle();
        }

        float dist = Mathf.Clamp(f.opponentDistance / 10f, 0f, 1f);
        float distScore = oppDist.Evaluate(dist);
        scores.Add(distScore);

        if (f.opponentAttackRemaining >= 0)
        {
            float arScore = attackRemaining.Evaluate(f.opponentAttackRemaining);
            scores.Add(arScore);
        }
    }

    public virtual void Execute(Fighter f, AIInputController controller) { }
}