namespace Structures;

public class Sphere : IEquatable<Sphere>
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

    public bool Equals(Sphere? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Center.Equals(other.Center) && Radius.Equals(other.Radius);
    }

    public double Distance(Ray ray)
    {
        return Vector3.Cross(ray.Direction, Center - ray.Origin).Magnitude();
    }

    // TODO: czy przeciecie to też stycznosc? jak tak to zle (<=)
    public bool Intersects(Ray ray)
    {
        return Distance(ray) < Radius;
    }

    public static bool operator ==(Sphere a, Sphere b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Sphere a, Sphere b)
    {
        return !(a == b);
    }

    // http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html
    public List<Vector3> Intersection(Ray ray)
    {
        var L = new Vector3(ray.Origin, Center);
        var tc = L.Dot(ray.Direction);

        var d = Math.Sqrt(L.MagnitudeSquared() - tc * tc);
        if (d > Radius) return new List<Vector3>();

        var t1c = Math.Sqrt(Radius * Radius - d * d);
        var t1 = tc - t1c;
        var t2 = tc + t1c;
        var retList = new List<Vector3>();
        if (t1c == 0)
        {
            retList.Add(ray.PointAtDistanceFromOrigin(t1));
            return retList;
        }

        if (t1 > 0) retList.Add(ray.PointAtDistanceFromOrigin(t1));

        if (t2 > 0) retList.Add(ray.PointAtDistanceFromOrigin(t2));

        return retList;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Sphere);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Center, Radius);
    }
}