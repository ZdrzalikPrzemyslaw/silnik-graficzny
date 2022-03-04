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

    public double Distance(Ray ray)
    {
        return Vector3.Cross(ray.Direction, this.Center - ray.Origin).Magnitude();
    }
    
    // TODO: czy przeciecie to też stycznosc? jak tak to zle (<=)
    public bool Intersects(Ray ray)
    {
        return Distance(ray) < Radius;
    }

    // TODO:
    public Vector3 Intersection(Ray ray)
    {
        return Vector3.Zero();
    }

    public Vector3 Center { get; set; }
    public double Radius { get; set; }
}