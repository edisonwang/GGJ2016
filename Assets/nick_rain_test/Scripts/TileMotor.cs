using RAIN.Core;
using RAIN.Motion;
using RAIN.Serialization;

[RAINSerializableClass, RAINElement("Tile Motor")]
public class TileMotor : BasicMotor
{

    //private Vector3 tileTarget;
    //private MoveLookTarget prevMoveTarget;
    //private bool moving;

    public override bool Move()
    {
        //bool result = false;
        //Debug.Log("Motor Move");
        MoveTarget.CloseEnoughDistance = 0;
        //moving = true;
        /*
        if (prevMoveTarget == null)
        {
            result = base.Move();
            tileTarget = AI.Navigator.CurrentPath.PathPoints[AI.Navigator.NextWaypoint];
            prevMoveTarget = MoveTarget;
        }
        else
        {
            if ()
        }

        return result;
        */
        return base.Move();
    }

    public override bool IsAt(MoveLookTarget aTarget)
    {
        //Debug.Log("Motor IsAt");
        return base.IsAt(aTarget);
    }

    public override void ApplyMotionTransforms()
    {
        /*Debug.Log("ApplyMotionTransforms");
        if (moving)
        {
            if (AI.Navigator.CurrentPath == null)
            {
                return;
            }
            var target = AI.Navigator.CurrentPath.PathPoints[AI.Navigator.NextWaypoint - 1];
            var targetDisplacement = target - AI.Body.transform.position;
            AI.Body.transform.position = AI.Body.transform.position + (targetDisplacement.normalized * Speed * AI.DeltaTime);
            AI.Body.transform.rotation.SetLookRotation(targetDisplacement, Vector3.up);
        }
        moving = false;*/

        //base.ApplyMotionTransforms();
        /*AI.Body.transform.rotation.SetLookRotation(tileTarget - AI.Body.transform.position, Vector3.up);

        var vel = AI.Kinematic.Velocity;
        var normalizedVel = vel.normalized;
        //MoveLookTarget mlt = AI.Navigator.GetNextPathWaypoint(false, false);
        //mlt.
        if (Mathf.Abs(normalizedVel.x) >= Mathf.Abs(normalizedVel.z))
        {
            AI.Kinematic.Velocity = new Vector3(vel.x, 0, 0);
        }
        else
        {
            AI.Kinematic.Velocity = new Vector3(0, 0, vel.z);
        }
        //AI.Kinematic.Velocity = AI.Body.transform.forward * AI.Kinematic.Velocity.magnitude;
       
        //base.ApplyMotionTransforms();
        //AI.Body.transform.position = new Vector3(AI.Body.transform.position.x, 0, AI.Body.transform.position.z);

        if (Mathf.Abs(normalizedVel.x) >= Mathf.Abs(normalizedVel.z))
        {
            //AI.Body.transform.position = new Vector3(AI.Body.transform.position.x, 0, Mathf.Abs(AI.Body.transform.position.z));
        }
        else
        {
            //AI.Body.transform.position = new Vector3(Mathf.Abs(AI.Body.transform.position.x), 0, AI.Body.transform.position.z);
        }*/
    }
}
