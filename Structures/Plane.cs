namespace Structures;

public class Plane : IEquatable<Plane>
{
    private Vector3? _center;

    public Plane(Vector3 normal, double distance)
    {
        Distance = distance;
        Normal = normal.GetNormalized();
    }

    public Plane(Vector3 inNormal, Vector3 point) : this(inNormal, GetDistanceAlongNormal(inNormal, point))
    {
    }

    public double Distance { get; } // Do 0, 0, 0

    public Vector3 Normal { get; }

    public Vector3 Center
    {
        get
        {
            if (_center is null) _center = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
            return _center;
        }
    }

    public bool Equals(Plane? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Distance.Equals(other.Distance) && Normal.Equals(other.Normal);
    }

    public Plane Flipped()
    {
        return new Plane(-Normal, -Distance);
    }

    public static bool operator ==(Plane a, Plane b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Plane a, Plane b)
    {
        return !(a == b);
    }


    public bool Intersects(Ray ray)
    {
        var dot = Normal.Dot(ray.Direction);
        if (Math.Abs(dot) > 0.0001f)
        {
            var p = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
            var t = (p - ray.Origin).Dot(Normal) / dot;
            if (t >= 0) return true;
        }

        return false;
    }

    private static double GetDistanceAlongNormal(Vector3 inNormal, Vector3 point)
    {
        if (inNormal.Dot(point) < 0)
            return -(inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
        return (inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
    }

    public Vector3? Intersection(Ray ray)
    {
        if (Intersects(ray))
        {
            var d = Center.Dot(-Normal);
            var t = -(d + ray.Origin.Z * Normal.Z + ray.Origin.Y * Normal.Y + ray.Origin.X * Normal.X)
                    / (ray.Direction.Z * Normal.Z + ray.Direction.Y * Normal.Y + ray.Direction.X * Normal.X);
            return ray.Origin + t * ray.Direction;
        }

        return null;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Plane);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Distance, Normal);
    }
}