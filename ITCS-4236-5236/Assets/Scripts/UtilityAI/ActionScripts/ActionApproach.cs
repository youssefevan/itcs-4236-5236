using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionApproach")]
public class ActionApproach : AIAction
{
    public override void EvaluateContext(Fighter f, AIInputController controller)
    {
        base.EvaluateContext(f, controller);

        if (f.opponentState is Attack)
        {
            scores.Add(0.5f);
        }

        if (f.opponentApproaching)
        {
            scores.Add(0.6f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.Move(f.facing);
    }
}
