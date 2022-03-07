namespace Structures;

public class Sphere : IEquatable<Sphere>
{
    /// <summary>
    ///     Creates new Sphere starting at {0, 0, 0} with radius equal to 0.
    /// </summary>
    public Sphere()
    {
        Center = Vector3.Zero();
        Radius = 0;
    }

    /// <summary>
    ///     Creates new Sphere with given center location and radius.
    /// </summary>
    /// <param name="center">Given center location</param>
    /// <param name="radius">Given radius</param>
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
    public bool Intersects(Ray ray)
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
        return $"Plane(Center: {Center}, Radius: {Radius})";
    }
}