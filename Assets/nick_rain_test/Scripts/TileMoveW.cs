using UnityEngine;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Navigation.Waypoints;
using RAIN.Motion;

[RAINAction]
public class TileMoveW : RAINAction
{
    private MoveLookTarget nextNavTarget;
    private Vector3 targetTile;
    private Vector3 moveDirection;
    private bool pathFound;

    public override void Start(AI ai)
    {
        pathFound = false;
        var nt = ai.WorkingMemory.GetItem<MoveLookTarget>("moveTarget");
        if (nt != null)
        {
            ai.Motor.MoveTarget = nt;
            nextNavTarget = nt;
        }
        ai.Motor.Move();
    }

    public override ActionResult Execute(AI ai)
    {

        if (!pathFound)
        {
            if (ai.Navigator.CurrentPath != null)
            {
                if (ai.Navigator.NextWaypoint == ai.Navigator.CurrentPath.PathPoints.Count)
                {
                    ai.Navigator.NextWaypoint -= 1;
                }
                var nextWaypointPos = ai.Navigator.CurrentPath.PathPoints[ai.Navigator.NextWaypoint];
                var nextWaypointDir = (nextWaypointPos - ai.Body.transform.position).normalized;

                if (Mathf.Approximately(nextWaypointDir.x, 0) && Mathf.Approximately(nextWaypointDir.z, 0))
                {
                    pathFound = true;
                }
                else if (Mathf.Abs(nextWaypointDir.x) >= Mathf.Abs(nextWaypointDir.z))
                {
                    var x = Mathf.Ceil(Mathf.Abs(nextWaypointDir.x)) * Mathf.Sign(nextWaypointDir.x);
                    targetTile = new Vector3(Mathf.Round(ai.Body.transform.position.x + x),
                                             0,
                                             Mathf.Round(ai.Body.transform.position.z));
                    moveDirection = new Vector3(Mathf.Sign(x), 0, 0);
                    pathFound = true;
                }
                else
                {
                    var z = Mathf.Ceil(Mathf.Abs(nextWaypointDir.z)) * Mathf.Sign(nextWaypointDir.z);
                    targetTile = new Vector3(Mathf.Round(ai.Body.transform.position.x),
                                             0,
                                             Mathf.Round(ai.Body.transform.position.z + z));
                    moveDirection = new Vector3(0, 0, Mathf.Sign(z));
                    pathFound = true;
                }
            }
        }

        if (pathFound)
        {
            ai.Motor.Move();

            if (Vector3.Dot((targetTile - ai.Body.transform.position).normalized, moveDirection) < -0.7f ||
                ai.Body.transform.position == targetTile)
            {
                ai.Body.transform.position = targetTile;
                pathFound = false;

                /*if (ai.Body.transform.position == nextNavTarget.Position)
                {
                    ai.Body.transform.rotation = Quaternion.Euler(nextNavTarget.Orientation);
                }*/
                return ActionResult.SUCCESS;
            }
            else
            {
                var targetDisplacement = targetTile - ai.Body.transform.position;
                //Smooth move
                //ai.Body.transform.position = ai.Body.transform.position + (targetDisplacement.normalized * ai.Motor.Speed * ai.DeltaTime);
                //Snap move
                ai.Body.transform.position = ai.Body.transform.position + (targetDisplacement.normalized);
                ai.Body.transform.position = new Vector3(ai.Body.transform.position.x, 0, ai.Body.transform.position.z);

                if (!ai.WorkingMemory.GetItem<bool>("directionOverride"))
                {
                    ai.WorkingMemory.SetItem("direction", new Vector2(targetDisplacement.x, targetDisplacement.z));
                }
            }
        }
        return ActionResult.RUNNING;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }

}
