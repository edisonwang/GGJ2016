using UnityEngine;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation.Targets;

[RAINAction]
public class FootstepSoundAction : RAINAction
{
    private FootstepAudioController fac;

    public override void Start(AI ai)
    {
        fac = ai.Body.GetComponentInParent<FootstepAudioController>();
    }

    public override ActionResult Execute(AI ai)
    {
        fac.PlayFootstep();
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {

    }
}