namespace Structures;

public class Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction.GetNormalized();
    }

    public Ray() : this(Vector3.Zero(), Vector3.Zero())
    {
    }

    public Vector3 Origin { get; }
    public Vector3 Direction { get; }

    public Vector3 PointAtDistanceFromOrigin(double distance)
    {
        return Origin + Direction * distance;
    }
}