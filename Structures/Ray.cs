namespace Structures;

public class Ray
{
    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }

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
}