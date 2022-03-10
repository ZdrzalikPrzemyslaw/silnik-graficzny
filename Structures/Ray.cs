namespace Structures;

public class Ray : IEquatable<Ray>
{
    /// <summary>
    ///     Creates the ray with given values of origin and direction.
    /// </summary>
    /// <param name="origin">Point where the ray starts.</param>
    /// <param name="direction">Direction to infinity of the ray.</param>
    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction.GetNormalized();
    }

    /// <summary>
    ///     Creates the ray that starts in (0, 0, 0) and with no direction.
    /// </summary>
    public Ray() : this(Vector3.Zero(), Vector3.Zero())
    {
    }

    /// <summary>
    ///     The start point of the ray.
    /// </summary>
    public Vector3 Origin { get; }

    /// <summary>
    ///     The direction to infinity of the ray.
    /// </summary>
    public Vector3 Direction { get; }

    /// <inheritdoc />
    public bool Equals(Ray? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Origin.Equals(other.Origin) && Direction.Equals(other.Direction);
    }

    /// <summary>
    ///     Creates new ray with inverted direction and return te results.
    /// </summary>
    /// <returns>The new ray with inverted direction.</returns>
    public Ray Inverse()
    {
        return new Ray(Origin, Direction.Inverse());
    }

    /// <summary>
    ///     Checks equality between two rays.
    /// </summary>
    /// <param name="a">The left operand of the equality.</param>
    /// <param name="b">The right operand of the equality.</param>
    /// <returns>True if rays are equals, false if rays are unequals.</returns>
    public static bool operator ==(Ray a, Ray b)
    {
        return a.Equals(b);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Origin, Direction);
    }

    /// <summary>
    ///     Checks inequality between two rays.
    /// </summary>
    /// <param name="a">The left operand of the inequality.</param>
    /// <param name="b">The right operand of the inequality.</param>
    /// <returns>False if rays are equals, true if rays are unequals.</returns>
    public static bool operator !=(Ray a, Ray b)
    {
        return !(a == b);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Ray(Origin: {Origin}, Direction: {Direction})";
    }

    /// <summary>
    ///     Calculates the coordinates of point on the ray that is in distance from Origin and returns the result.
    /// </summary>
    /// <param name="distance">Distance between point and Origin</param>
    /// <returns>Point in distance from Origin</returns>
    public Vector3 PointAtDistanceFromOrigin(double distance)
    {
        return Origin + Direction * distance;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Ray);
    }
}