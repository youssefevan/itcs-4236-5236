using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionCrouch")]
public class ActionCrouch : AIAction
{
    public override void EvaluateContext(Fighter f)
    {
        base.EvaluateContext(f);

        if (f.opponentState is Attack)
        {
            scores.Add(0.8f);
        }
        else
        {
            scores.Add(0.3f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.SetInputs(0, 0, false, false, true, false, false);
    }
}
