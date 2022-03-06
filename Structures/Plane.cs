namespace Structures;

public class Plane
{
    public Plane(Vector3 inNormal, double inDistance)
    {
        distance = inDistance;
        normal = inNormal.GetNormalized();
    }

    public Plane(Vector3 inNormal, Vector3 point) : this(inNormal, GetDistanceAlongNormal(inNormal, point))
    {
    }

    public double distance { get; set; } // to 0, 0, 0
    public Vector3 normal { get; set; }

    private static double GetDistanceAlongNormal(Vector3 inNormal, Vector3 point)
    {
        if (inNormal.Dot(point) < 0)
            return -(inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
        return (inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
    }

    // TODO:
    public Vector3 Intersection(Ray ray)
    {
        return Vector3.Zero();
    }
}