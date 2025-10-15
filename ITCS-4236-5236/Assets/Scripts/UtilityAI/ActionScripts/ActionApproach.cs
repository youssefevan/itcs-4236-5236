using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionApproach")]
public class ActionApproach : AIAction
{
    public override void EvaluateContext(Fighter f)
    {
        base.EvaluateContext(f);

        if (f.opponentState is Attack)
        {
            scores.Add(0.5f);
        }
        else
        {
            scores.Add(0.9f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.Move(f.facing);
    }
}
