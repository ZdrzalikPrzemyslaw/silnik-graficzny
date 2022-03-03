namespace Structures;

public class Sphere
{
    public Sphere()
    {
        Center = Vector3.Zero();
        Radius = 0;
    }

    public Sphere(Vector3 center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public Vector3 Center { get; set; }
    public double Radius { get; set; }
}