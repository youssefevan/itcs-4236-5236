using System.Linq;
using NUnit.Framework;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "AIAction/ActionDodge")]
public class ActionDodge : AIAction
{
    public override void EvaluateContext(Fighter f, AIInputController controller)
    {
        base.EvaluateContext(f, controller);

        if (f.IsGrounded() == true)
        {
            scores.Add(0.0f);
        }

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
            scores.Add(0.3f);
        }

        if (f.opponentState is Block)
        {
            scores.Add(0.2f);
        }
    }

    public override void Execute(Fighter f, AIInputController controller)
    {
        controller.SetInputs(f.facing, 0, false, false, false, false, false, true);
    }
}
