using UnityEngine;
using RAIN.Navigation;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Navigation.Pathfinding;
using RAIN.Motion;

[RAINSerializableClass, RAINElement("Tile Navigator")]
public class TileNavigator : BasicNavigator
{
    public MoveLookTarget mlt;
    // Seems to not get used
    public override bool GetPathTo(Vector3 aTargetPosition, int aMaxPathfindSteps, float aMaxPathLength, bool aAllowOffGraphMovement, out RAINPath aPath)
    {
        //Debug.Log("Called GetPathTo");
        return base.GetPathTo(aTargetPosition, aMaxPathfindSteps, aMaxPathLength, aAllowOffGraphMovement, out aPath);
    }

    public override MoveLookTarget GetNextPathWaypoint(MoveLookTarget aPathTarget, bool aAllow3DMovement, bool aAllowOffGraphMovement, MoveLookTarget aCachedMoveLookTarget = null)
    {
        mlt = base.GetNextPathWaypoint(aPathTarget, aAllow3DMovement, aAllowOffGraphMovement, aCachedMoveLookTarget);
        //Debug.Log("Called GetNextPathWaypoint");
        
        return mlt;
    }

    public override bool GetPathToMoveTarget(MoveLookTarget aPathTarget, bool allowOffGraphMovement, out RAINPath path)
    {
        //Debug.Log("Called GetPathToMoveTarget");
        var result = base.GetPathToMoveTarget(aPathTarget, allowOffGraphMovement, out path);

        for (int i = 0; i < path.PathPoints.Count; i++)
        {
            var p = path.PathPoints[i];
            path.PathPoints[i] = new Vector3(Mathf.Round(p.x), 0, Mathf.Round(p.z));
        }

        if (path.PathNodes.Count == 2)
        {
            return result;
        }

        for (int i = 0; i < path.PathNodes.Count - 1; i++)
        {
            if (path.PathPoints[i] == path.PathPoints[i + 1])
            {
                path.RemovePathNode(i);
                i--;
            }
        }

        bool sameX = false;
        bool sameZ = false;
        for (int i = 0; i < path.PathNodes.Count - 1; i++)
        {
            //if (Mathf.Approximately(path.PathPoints[i].x, AI.Body.transform.position.x) &&
            //    Mathf.Approximately(path.PathPoints[i].z, AI.Body.transform.position.z))
            //{
                //path.RemovePathNode(i);
                //i--;
            //}
            if (sameX)
            {
                if (path.PathPoints[i].x == path.PathPoints[i + 1].x)
                {
                    path.RemovePathNode(i);
                    i--;
                }
                else
                {
                    sameX = false;
                }
            }
            else if (sameZ)
            {
                if (path.PathPoints[i].z == path.PathPoints[i + 1].z)
                {
                    path.RemovePathNode(i);
                    i--;
                }
                else
                {
                    sameZ = false;
                }
            }
            if (!sameX && !sameZ)
            {
                if (path.PathPoints[i].x == path.PathPoints[i + 1].x)
                {
                    sameX = true;
                }
                else if (path.PathPoints[i].z == path.PathPoints[i + 1].z)
                {
                    sameZ = true;
                }
            }
        }

        return result;
    }

    public override bool IsAt(MoveLookTarget aTarget)
    {
        //Debug.Log("Called IsAt");
        return base.IsAt(aTarget);
    }

}
