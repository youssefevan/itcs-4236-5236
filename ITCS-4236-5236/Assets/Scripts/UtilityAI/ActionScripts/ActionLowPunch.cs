using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionLowPunch")]
public class ActionLowPunch : AIAction
{
    public override void EvaluateContext(Fighter f, AIInputController controller)
    {
        base.EvaluateContext(f, controller);

        float thisActionInQueue = 0f;
        foreach (var action in f.previousActions)
        {
            if (action == this)
            {
                thisActionInQueue += 1f;
            }
        }

        scores.Add(1f / (thisActionInQueue + 1f));

        if (f.opponentState is Attack)
        {
            scores.Add(0.5f);
        }

        if (f.opponentState is Block)
        {
            scores.Add(0.7f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        f.AddPreviousAction(this);
        controller.SetInputs(0, 0, false, true, false, true, false, false);
    }
}
