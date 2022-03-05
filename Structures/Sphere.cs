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

    // http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html
    public List<Vector3> Intersection(Ray ray)
    {
        var L = new Vector3(ray.Origin, Center);
        var tc = L.Dot(ray.Direction);

        var d = Math.Sqrt(L.MagnitudeSquared() - tc * tc);
        if (d > Radius)
        {
            return new List<Vector3>();
        }

        var t1c = Math.Sqrt(Radius * Radius - d * d);
        var t1 = tc - t1c;
        var t2 = tc + t1c;
        if (t1c == 0)
        {
            return new List<Vector3>{ray.PointAtDistanceFromOrigin(t1)};
        }
        return new List<Vector3>{ray.PointAtDistanceFromOrigin(t1), ray.PointAtDistanceFromOrigin(t2)};
    }

    public Vector3 Center { get; set; }
    public double Radius { get; set; }
}