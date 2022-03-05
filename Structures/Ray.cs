namespace Structures;

public class Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Ray()
    {
        Origin = Vector3.Zero();
        Direction = Vector3.Zero();
    }

    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }

    public Vector3 PointAtDistanceFromOrigin(double distance)
    {
        return Origin + Direction * distance;
    }
}