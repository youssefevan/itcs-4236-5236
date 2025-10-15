using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

[CreateAssetMenu(menuName = "AIAction")]
public class AIAction : ScriptableObject
{
    public AnimationCurve oppDist;
    public AnimationCurve attackRemaining;
    protected List<float> scores = new List<float>();

    public float CalculateScore(Fighter f)
    {
        scores.Clear();
        EvaluateContext(f);

        float finalScore = 1f;
        foreach (var score in scores)
        {
            finalScore *= score;
        }

        finalScore *= Random.Range(0.8f, 1.0f);

        Debug.Log(name + ": " + finalScore);
        return finalScore;
    }

    public virtual void EvaluateContext(Fighter f)
    {
        if (f.stateManager.GetCurrentState() is Attack)
        {
            scores.Add(0f);
        }
        else if (f.stateManager.GetCurrentState() is Jumpsquat)
        {
            scores.Add(0f);
        }
        else if (f.stateManager.GetCurrentState() is Land)
        {
            scores.Add(0f);
        }
        else if (f.stateManager.GetCurrentState() is Hitstun)
        {
            scores.Add(0f);
        }
        else if (f.stateManager.GetCurrentState() is Knockback)
        {
            scores.Add(0f);
        }

        float dist = Mathf.Clamp(f.opponentDistance / 10f, 0f, 1f);
        float distScore = oppDist.Evaluate(dist);
        scores.Add(distScore);

        float arScore = attackRemaining.Evaluate(f.opponentAttackRemaining);
        scores.Add(arScore);
    }

    public virtual void Execute(Fighter f, AIInputController controller) { } // Sequence of inputs
}