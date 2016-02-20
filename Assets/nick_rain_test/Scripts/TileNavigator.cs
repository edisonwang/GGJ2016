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
        return base.GetPathTo(aTargetPosition, aMaxPathfindSteps, aMaxPathLength, aAllowOffGraphMovement, out aPath);
    }

    public override MoveLookTarget GetNextPathWaypoint(MoveLookTarget aPathTarget, bool aAllow3DMovement, bool aAllowOffGraphMovement, MoveLookTarget aCachedMoveLookTarget = null)
    {
        mlt = base.GetNextPathWaypoint(aPathTarget, aAllow3DMovement, aAllowOffGraphMovement, aCachedMoveLookTarget);

        if (CurrentPath == null || CurrentPath.PathPoints == null)
        {
            Debug.Log("Path points null");
            return mlt;
        }

        if (NextWaypoint < CurrentPath.PathPoints.Count - 1 &&
            AI.Body.transform.position == CurrentPath.PathPoints[NextWaypoint])
        {
            NextWaypoint += 1;
        }
        
        return mlt;
    }

    public override bool GetPathToMoveTarget(MoveLookTarget aPathTarget, bool allowOffGraphMovement, out RAINPath path)
    {
        // Find path within Nav Mesh
        var result = base.GetPathToMoveTarget(aPathTarget, allowOffGraphMovement, out path);

        // Round points to nearest unit on grid
        for (int i = 0; i < path.PathPoints.Count; i++)
        {
            var p = path.PathPoints[i];
            path.PathPoints[i] = new Vector3(Mathf.Round((Mathf.Abs(p.x) - 0.03f) * Mathf.Sign(p.x)),
                                             0,
                                             Mathf.Round((Mathf.Abs(p.z) - 0.03f) * Mathf.Sign(p.z)));
        }

        // Return if the path is too small to cull nodes
        if (path.PathNodes.Count == 2)
        {
            return result;
        }

        // Remove duplicate nodes
        for (int i = 0; i < path.PathNodes.Count - 1; i++)
        {
            if (path.PathPoints[i] == path.PathPoints[i + 1])
            {
                path.RemovePathNode(i);
                i--;
            }
        }

        // Remove points that are on the same line except the first and last
        bool sameX = false;
        bool sameZ = false;
        for (int i = 0; i < path.PathNodes.Count - 1; i++)
        {
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

        // If the start and end points are on the same line, remove all inbetween points
        if (Mathf.Approximately(path.PathPoints[0].x, path.PathPoints[path.PathPoints.Count - 1].x) ||
            Mathf.Approximately(path.PathPoints[0].z, path.PathPoints[path.PathPoints.Count - 1].z))
        {
            for (int i = 1; i < path.PathPoints.Count - 1; i++)
            {
                path.RemovePathNode(i);
                --i;
            }
        }

        return result;
    }

    public override bool IsAt(MoveLookTarget aTarget)
    {
        return base.IsAt(aTarget);
    }

}
