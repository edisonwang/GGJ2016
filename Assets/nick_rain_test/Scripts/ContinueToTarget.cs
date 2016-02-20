using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Representation;

[RAINDecision]
public class ContinueToTarget : RAINDecision
{

    public Expression target;

    public override void Start(AI ai)
    {
        ai.Motor.MoveTarget.NavigationTarget = target.Evaluate<NavigationTarget>(ai.DeltaTime, ai.WorkingMemory);
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
