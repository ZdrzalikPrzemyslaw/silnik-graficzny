namespace Structures;

public class Sphere
{
    public Vector3 Center { get; set; }
    public Vector3 Radius { get; set; }

    public Sphere()
    {
        Center = Vector3.Zero();
        Radius = Vector3.Zero();
    }

    public Sphere(Vector3 center, Vector3 radius)
    {
        Center = center;
        Radius = radius;
    }
}