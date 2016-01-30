using UnityEngine;
using System.Collections;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Motion;

[RAINAction]
public class TileMove : RAINAction
{
    public Expression target;

    private Vector3 targetTile;
    private Vector3 moveDirection;
    private bool movingToTarget;

    public override void Start(AI ai)
    {
        movingToTarget = false;
        //targetTile = target.Evaluate<Vector3>(ai.DeltaTime, ai.WorkingMemory);
        ai.Motor.MoveTarget = target.Evaluate<MoveLookTarget>(ai.DeltaTime, ai.WorkingMemory);
        //targetTile = ai.Motor.MoveTarget.Position;
        //base.Start(ai);
        ai.Motor.Move();
    }

    public override ActionResult Execute(AI ai)
    {
        
        if (!movingToTarget)
        {
            if (ai.Navigator.CurrentPath != null)
            {
                var nextWaypointPos = ai.Navigator.CurrentPath.PathPoints[ai.Navigator.NextWaypoint];
                var nextWaypointDir = (nextWaypointPos - ai.Body.transform.position).normalized;
                if (Mathf.Approximately(nextWaypointDir.x, 0) && Mathf.Approximately(nextWaypointDir.z, 0))
                {
                    movingToTarget = false;
                    return ActionResult.RUNNING;
                }
                else if (nextWaypointDir.x >= nextWaypointDir.z)
                {
                    var x = Mathf.Ceil(Mathf.Abs(nextWaypointDir.x)) * Mathf.Sign(nextWaypointDir.x);
                    targetTile = new Vector3(ai.Body.transform.position.x + x,
                                             0,
                                             Mathf.Round(ai.Body.transform.position.z));
                    moveDirection = new Vector3(Mathf.Sign(x), 0, 0);
                    movingToTarget = true;
                }
                else
                {
                    var z = Mathf.Ceil(Mathf.Abs(nextWaypointDir.z)) * Mathf.Sign(nextWaypointDir.z);
                    targetTile = new Vector3(Mathf.Round(ai.Body.transform.position.x),
                                             0,
                                             ai.Body.transform.position.z + z);
                    moveDirection = new Vector3(0, 0, Mathf.Sign(z));
                    movingToTarget = true;
                }
            }
        }
        
        if (movingToTarget)
        {
            ai.Motor.Move();

            if (Vector3.Dot(targetTile - ai.Body.transform.position, moveDirection) > 0.9f)
            {
                ai.Body.transform.position = targetTile;
                movingToTarget = false;
                return ActionResult.SUCCESS;
            }

            /*if (Mathf.Approximately(ai.Body.transform.position.x, targetTile.x) &&
                Mathf.Approximately(ai.Body.transform.position.z, targetTile.z))
            {
                ai.Body.transform.position = targetTile;
                movingToTarget = false;
                return ActionResult.SUCCESS;
            }*/
            
        }
        return ActionResult.RUNNING;
        //return base.Execute(ai);
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }

}
