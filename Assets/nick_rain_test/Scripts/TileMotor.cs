using RAIN.Core;
using RAIN.Motion;
using RAIN.Serialization;

[RAINSerializableClass, RAINElement("Tile Motor")]
public class TileMotor : BasicMotor
{

    public override bool Move()
    {
        MoveTarget.CloseEnoughDistance = 0;
        return base.Move();
    }

    public override bool IsAt(MoveLookTarget aTarget)
    {
        return base.IsAt(aTarget);
    }

    public override void ApplyMotionTransforms()
    {

    }
}
