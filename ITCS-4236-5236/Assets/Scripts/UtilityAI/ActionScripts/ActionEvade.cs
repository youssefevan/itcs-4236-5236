using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionEvade")]
public class ActionEvade : AIAction
{
    public override void EvaluateContext(Fighter f, AIInputController controller)
    {
        base.EvaluateContext(f, controller);

        if (f.opponentState is Attack)
        {
            scores.Add(1.0f);
        }
        else
        {
            scores.Add(0.4f);
        }

        if (!f.opponentApproaching)
        {
            scores.Add(0.4f);
        }

        if (f.opponentState is Crouch)
        {
            scores.Add(0.5f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.Move(-f.facing);
    }
}
