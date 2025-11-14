using System.Linq;
using NUnit.Framework;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionLowBlock")]
public class ActionLowBlock : AIAction
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

        if (f.opponentState is Crouch)
        {
            scores.Add(1.0f);
        }
        else
        {
            scores.Add(0.5f);
        }

        if (!f.opponentApproaching)
        {
            scores.Add(0.3f);
        }

        if (f.opponentState is Crouch)
        {
            scores.Add(0.5f);
        }

        if (f.opponentState is Block || f.opponentState is LowBlock)
        {
            scores.Add(0.3f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.SetInputs(0, 0, false, true, true, false, false, false);
    }
}
