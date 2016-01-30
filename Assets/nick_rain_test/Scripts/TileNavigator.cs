using UnityEngine;
using System.Collections;
using RAIN.Navigation;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Navigation;
using RAIN.Navigation.Pathfinding;
using RAIN.Motion;

[RAINSerializableClass, RAINElement("Tile Navigator")]
public class TileNavigator : BasicNavigator
{

    // Seems to not get used
    public override bool GetPathTo(Vector3 aTargetPosition, int aMaxPathfindSteps, float aMaxPathLength, bool aAllowOffGraphMovement, out RAINPath aPath)
    {
        Debug.Log("Called GetPathTo");
        return base.GetPathTo(aTargetPosition, aMaxPathfindSteps, aMaxPathLength, aAllowOffGraphMovement, out aPath);
    }

    public override MoveLookTarget GetNextPathWaypoint(MoveLookTarget aPathTarget, bool aAllow3DMovement, bool aAllowOffGraphMovement, MoveLookTarget aCachedMoveLookTarget = null)
    {
        Debug.Log("Called GetNextPathWaypoint");
        return base.GetNextPathWaypoint(aPathTarget, aAllow3DMovement, aAllowOffGraphMovement, aCachedMoveLookTarget);
    }

    public override bool GetPathToMoveTarget(MoveLookTarget aPathTarget, bool allowOffGraphMovement, out RAINPath path)
    {
        Debug.Log("Called GetPathToMoveTarget");
        var result = base.GetPathToMoveTarget(aPathTarget, allowOffGraphMovement, out path);

        for (int i = 0; i < path.PathPoints.Count; i++)
        {
            var p = path.PathPoints[i];
            path.PathPoints[i] = new Vector3(Mathf.Round(p.x), 0, Mathf.Round(p.z));
        }

        return result;
    }

    public override bool IsAt(MoveLookTarget aTarget)
    {
        Debug.Log("Called IsAt");
        return base.IsAt(aTarget);
    }

}
