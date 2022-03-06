namespace Structures;

public class Plane
{
    public Plane(Vector3 normal, double distance)
    {
        Distance = distance;
        Normal = normal.GetNormalized();
        Center = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
    }

    public Plane(Vector3 inNormal, Vector3 point) : this(inNormal, GetDistanceAlongNormal(inNormal, point))
    {
    }

    public double Distance { get; set; } // to 0, 0, 0

    public Vector3 Normal { get; set; }

    private Vector3 Center { get; }

    public double Z { get; set; }

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

    public Vector3 Intersection(Ray ray)
    {
        var d = Center.Dot(-Normal);
        var t = -(d + ray.Origin.Z * Normal.Z + ray.Origin.Y * Normal.Y + ray.Origin.X * Normal.X)
                / (ray.Direction.Z * Normal.Z + ray.Direction.Y * Normal.Y + ray.Direction.X * Normal.X);
        return ray.Origin + t * ray.Direction;
    }
}