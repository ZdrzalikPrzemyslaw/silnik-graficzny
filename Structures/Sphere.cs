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
        return Vector3.Cross(ray.Direction, Center - ray.Origin).Magnitude();
    }
    
    public bool Intersects(Ray ray)
    {
        return Distance(ray) < Radius;
    }

    // TODO:
    public Vector3 Intersection(Ray ray)
    {
        double t = (new Vector3(Center - ray.Origin)).Dot(ray.Direction);
        Vector3 p = ray.Origin + ray.Direction * t;
        Vector3 vec = new Vector3(Center - p);
        double x = Math.Sqrt(Radius * Radius - vec.Cross(vec).Magnitude());
        double t1 = t - x;
        double t2 = t + x;
    }

    public Vector3 Center { get; set; }
    public double Radius { get; set; }
}