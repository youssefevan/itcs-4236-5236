using System.Linq;
using NUnit.Framework;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionBlock")]
public class ActionBlock : AIAction
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
            scores.Add(0.3f);
        }

        if (!f.opponentApproaching)
        {
            scores.Add(0.3f);
        }

        if (f.opponentState is Crouch)
        {
            scores.Add(0.5f);
        }

        if (f.opponentState is Block)
        {
            scores.Add(0.3f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.SetInputs(0, 0, false, false, true, false, false, false);
    }
}
