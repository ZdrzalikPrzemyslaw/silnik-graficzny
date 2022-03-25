using Structures.MathObjects;

namespace Structures.Figures;

public class Sphere : Figure, IEquatable<Sphere>
{
    /// <summary>
    ///     Creates new Sphere starting at {0, 0, 0} with radius equal to 0.
    /// </summary>
    public Sphere() : this(Vector3.Zero(), 0)
    {
    }

    public Sphere(Vector3 center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    /// <summary>
    ///     Center position of the Sphere.
    /// </summary>
    public Vector3 Center { get; set; }

    /// <summary>
    ///     Radius of the Sphere.
    /// </summary>
    public double Radius { get; set; }


    /// <inheritdoc />
    public bool Equals(Sphere? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Center.Equals(other.Center) && Radius.Equals(other.Radius);
    }

    /// <summary>
    ///     Computes the shortest distance between a Ray and this Sphere and returns the result.
    /// </summary>
    /// <param name="ray">Ray to compute the distance to</param>
    /// <returns>Shortest distance between Ray and Sphere.</returns>
    public double Distance(Ray ray)
    {
        return Vector3.Cross(ray.Direction, Center - ray.Origin).Magnitude();
    }

    /// <summary>
    ///     Checks whether a given Ray is tangent to or intersects this Sphere and returns the result.
    /// </summary>
    /// <param name="ray">Given Ray</param>
    /// <returns>True if intersects of is tangent, false otherwise.</returns>
    public override bool Intersects(Ray ray)
    {
        return Distance(ray) <= Radius;
    }


    /// <summary>
    ///     Checks whether two Spheres are equal and returns the result.
    /// </summary>
    /// <param name="a">First Sphere</param>
    /// <param name="b">Second Sphere</param>
    /// <returns>True if objects are equal, false otherwise.</returns>
    public static bool operator ==(Sphere a, Sphere b)
    {
        return a.Equals(b);
    }

    /// <summary>
    ///     Checks whether two Spheres are not equal and returns the result.
    /// </summary>
    /// <param name="a">First Sphere</param>
    /// <param name="b">Second Sphere</param>
    /// <returns>True if objects are not equal, false otherwise.</returns>
    public static bool operator !=(Sphere a, Sphere b)
    {
        return !(a == b);
    }

    /// <summary>
    ///     Finds the points of intersection between a given Ray and this Sphere and returns the result.
    ///     http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html
    /// </summary>
    /// <param name="ray">Given Ray</param>
    /// <returns>Empty list if no intersections, one element in list if tangent, two elements otherwise.</returns>
    // 
    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        var L = new Vector3(ray.Origin, Center);
        var tc = L.Dot(ray.Direction);

        var d = Math.Sqrt(L.MagnitudeSquared() - tc * tc);
        if (d > Radius) return new List<PointOfIntersection>();

        var t1c = Math.Sqrt(Radius * Radius - d * d);
        var t1 = tc - t1c;
        var t2 = tc + t1c;
        var retList = new List<PointOfIntersection>();
        if (t1c == 0)
        {
            retList.Add(new PointOfIntersection(this, ray.PointAtDistanceFromOrigin(t1)));
            return retList;
        }

        if (t1 > 0) retList.Add(new PointOfIntersection(this, ray.PointAtDistanceFromOrigin(t1)));

        if (t2 > 0) retList.Add(new PointOfIntersection(this, ray.PointAtDistanceFromOrigin(t2)));

        return retList;
    }

    public override bool Equals(Figure? other)
    {
        return Equals(other as Sphere);
    }

    //TODO: spojrzec czy tu [0] zawsze nie bedzie blizej
    public override PointOfIntersection? Intersection(Ray ray)
    {
        var points = Intersections(ray);
        if (points.Count == 2)
            return ray.Origin.Distance(points[0].Position) < ray.Origin.Distance(points[1].Position)
                ? points[0]
                : points[1];

        return points.Count == 1 ? points[0] : null;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Sphere);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Center, Radius);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Sphere (Center: {Center}, Radius: {Radius})";
    }
}