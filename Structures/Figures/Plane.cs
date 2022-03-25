using Structures.MathObjects;

namespace Structures.Figures;

public class Plane : Figure, IEquatable<Plane>
{
    /// <summary>
    ///     Coordinates of the point which is closest to {0, 0, 0}.
    /// </summary>
    private Vector3? _center;

    public Plane(Vector3 a, Vector3 b, Vector3 c) : this(CalculateNormalVector(a, b, c),
        GetDistanceAlongNormal(CalculateNormalVector(a, b, c), a))
    {
    }

    public Plane(Vector3 inNormal, double distance)
    {
        Distance = distance;
        Normal = inNormal.GetNormalized();
    }

    /// <summary>
    ///     Creates new Plane from a normal vector and a point which belongs to the plane.
    /// </summary>
    /// <param name="inNormal">Normal Vector</param>
    /// <param name="point">Point belonging to Plane</param>
    public Plane(Vector3 inNormal, Vector3 point) : this(inNormal, GetDistanceAlongNormal(inNormal, point))
    {
    }

    /// <summary>
    ///     Distance of the Plane to {0, 0, 0}.
    /// </summary>
    public double Distance { get; } // Do 0, 0, 0

    /// <summary>
    ///     The normal vector of the Plane.
    /// </summary>
    public Vector3 Normal { get; }

    /// <summary>
    ///     Coordinates of the point which is closest to {0, 0, 0}.
    /// </summary>
    public Vector3 Center
    {
        get
        {
            if (_center is null) _center = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
            return _center;
        }
    }

    /// <inheritdoc />
    public bool Equals(Plane? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Distance.Equals(other.Distance) && Normal.Equals(other.Normal);
    }

    public static Vector3 CalculateNormalVector(Vector3 a, Vector3 b, Vector3 c)
    {
        var ab = new Vector3(a, b);
        var ac = new Vector3(a, c);
        return ab.Cross(ac).GetNormalized();
    }

    /// <summary>
    ///     Creates a copy of the plane facing opposite direction and returns it.
    /// </summary>
    /// <returns>A copy of the plane facing opposite direction </returns>
    public Plane Flipped()
    {
        return new Plane(-Normal, -Distance);
    }

    /// <summary>
    ///     Checks equality between two planes.
    /// </summary>
    /// <param name="a">The left operand of the equality.</param>
    /// <param name="b">The right operand of the equality.</param>
    /// <returns>True if planes are equals, false if planes are unequals.</returns>
    public static bool operator ==(Plane a, Plane b)
    {
        return a.Equals(b);
    }

    /// <summary>
    ///     Checks inequality between two planes.
    /// </summary>
    /// <param name="a">The left operand of the inequality.</param>
    /// <param name="b">The right operand of the inequality.</param>
    /// <returns>False if planes are equals, true if planes are unequals.</returns>
    public static bool operator !=(Plane a, Plane b)
    {
        return !(a == b);
    }

    /// <summary>
    ///     Checks if the ray intersects the plane.
    /// </summary>
    /// <param name="ray">Ray to calculate the intersection point with the plane.</param>
    /// <returns>True if the ray and the plane intersects, false if they don't intersects.</returns>
    public override bool Intersects(Ray ray)
    {
        var dot = Normal.Dot(ray.Direction);
        if (Math.Abs(dot) > 0.0001f)
        {
            var p = new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance);
            var t = (p - ray.Origin).Dot(Normal) / dot;
            if (t >= 0) return true;
        }

        if (DistanceToPoint(ray.Origin) == 0) return true;

        return false;
    }

    /// <summary>
    ///     Checks if the ray intersects the plane.
    /// </summary>
    /// <param name="ray">Ray to calculate the intersection point with the plane.</param>
    /// <returns>True if the ray and the plane intersects, false if they don't intersects.</returns>
    private bool IsInfiniteIntersection(Ray ray)
    {
        if (DistanceToPoint(ray.Origin) != 0) return false;
        if (Math.Abs(Normal.Dot(ray.Direction)) < 0.0001f) return true;
        return false;
    }


    public double DistanceToPoint(Vector3 point)
    {
        return Normal.Dot(point - new Ray(Vector3.Zero(), Normal).PointAtDistanceFromOrigin(Distance));
    }

    /// <summary>
    ///     Calculates the shortest distance from (0, 0, 0) to plane and return the results.
    /// </summary>
    /// <param name="inNormal">The normal of the plane.</param>
    /// <param name="point">The point on the plane.</param>
    /// <returns>The distance from (0, 0, 0) to plane.</returns>
    public static double GetDistanceAlongNormal(Vector3 inNormal, Vector3 point)
    {
        if (inNormal.Dot(point) < 0)
            return -(inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
        return (inNormal * (inNormal.Dot(point) / inNormal.Dot(inNormal))).Magnitude();
    }

    /// <summary>
    ///     Calculates the intersection point of plane and <paramref name="ray" /> and return the results.
    /// </summary>
    /// <param name="ray">Ray to calculate the intersection point with the plane.</param>
    /// <returns>The point of the intersection or null if the point doesn't exist.</returns>
    //https://stackoverflow.com/a/53437900/17176800
    public override PointOfIntersection? Intersection(Ray ray)
    {
        if (!Intersects(ray)) return null;
        if (IsInfiniteIntersection(ray)) throw new InfiniteIntersectionsException();
        var d = Center.Dot(-Normal);
        var t = -(d + ray.Origin.Z * Normal.Z + ray.Origin.Y * Normal.Y + ray.Origin.X * Normal.X)
                / (ray.Direction.Z * Normal.Z + ray.Direction.Y * Normal.Y + ray.Direction.X * Normal.X);
        return new PointOfIntersection(this, ray.Origin + t * ray.Direction);
    }

    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        var point = Intersection(ray);
        if (point is null) return new List<PointOfIntersection>();
        // Inaczej zwroci sie 1 element null
        return new List<PointOfIntersection> { point };
    }

    public override bool Equals(Figure? other)
    {
        return Equals(other as Plane);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Plane);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Plane(Normal: {Normal}, Distance: {Distance})";
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Distance, Normal);
    }

    public class InfiniteIntersectionsException : Exception
    {
    }
}