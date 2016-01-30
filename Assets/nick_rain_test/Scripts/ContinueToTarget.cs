using UnityEngine;
using System.Collections;
using RAIN.Action;
using RAIN.Core;
using RAIN.Motion;
using RAIN.Navigation.Targets;

[RAINDecision]
public class ContinueToTarget : RAINDecision
{

    public override void Start(AI ai)
    {
        var nt = ai.WorkingMemory.GetItem<NavigationTarget>("moveTarget");
        if (nt != null)
        {
            ai.Motor.MoveTarget.NavigationTarget = nt;
        }
        base.Start(ai);
    }

    public override ActionResult Execute(AI ai)
    {
        if (_children.Count == 0)
        {
            return ActionResult.FAILURE;
        }
        int successes = 0;
        for (int i = 0; i < _children.Count; i++)
        {
            var result = _children[i].Run(ai);

            if (result == ActionResult.SUCCESS)
            {
                _children[i].Reset();
                successes++;
                //_children[i].Stop(ai);
                //_children[i].Start(ai);
            }
            else if (result == ActionResult.FAILURE)
            {
                return ActionResult.FAILURE;
            }
        }      

        if (ai.Body.transform.position == ai.Motor.MoveTarget.Position && successes == _children.Count)
        {
            return ActionResult.SUCCESS;
        }
        return ActionResult.RUNNING;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }

}
