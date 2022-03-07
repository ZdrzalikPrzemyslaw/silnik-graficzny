namespace Structures;

public class Vector3 : IEquatable<Vector3>
{
    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3(Vector3 vec1, Vector3 vec2)
    {
        X = vec2.X - vec1.X;
        Y = vec2.Y - vec1.Y;
        Z = vec2.Z - vec1.Z;
    }

    public Vector3(Vector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }

    public Vector3()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public bool Equals(Vector3? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }

    public static Vector3 Zero()
    {
        return new Vector3();
    }

    public override string ToString()
    {
        return $"Vector({X}, {Y}, {Z})";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Vector3);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public double Magnitude()
    {
        return Math.Sqrt(MagnitudeSquared());
    }

    public Vector3 ToPoint()
    {
        return new Vector3(this);
    }

    public double MagnitudeSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    public static Vector3 operator +(Vector3 a)
    {
        return new Vector3(a);
    }

    public static Vector3 operator -(Vector3 a)
    {
        return a * -1;
    }

    public static Vector3 operator *(Vector3 a, double k)
    {
        return new Vector3(a.X * k, a.Y * k, a.Z * k);
    }

    public static Vector3 operator *(double k, Vector3 a)
    {
        return new Vector3(a.X * k, a.Y * k, a.Z * k);
    }

    public static Vector3 operator /(Vector3 first, double k)
    {
        if (k == 0) throw new DivideByZeroException();
        return first * (1 / k);
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return a + -b;
    }

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    /// <summary>
    ///     Checks equality between two vectors.
    /// </summary>
    /// <param name="a">The left operand of the equality.</param>
    /// <param name="b">The right operand of the equality.</param>
    /// <returns>True if vectors are equals, false if vectors are unequals.</returns>
    public static bool operator ==(Vector3 a, Vector3 b)
    {
        return a.Equals(b);
    }

    /// <summary>
    ///     Checks inequality between two vectors.
    /// </summary>
    /// <param name="a">The left operand of the inequality.</param>
    /// <param name="b">The right operand of the inequality.</param>
    /// <returns>False if vectors are equals, true if vectors are unequals.</returns>
    public static bool operator !=(Vector3 a, Vector3 b)
    {
        return !(a == b);
    }

    /// <summary>
    ///     Calculates dot product on this and <paramref name="other" /> vector and returns the results.
    /// </summary>
    /// <param name="other">The right operand of the dot.</param>
    /// <returns>Vector resulting from dot.</returns>
    public double Dot(Vector3 other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    /// <summary>
    ///     Normalizes vector and returns the results.
    /// </summary>
    /// <returns>Normalized vector</returns>
    /// <exception cref="DivideByZeroException">Throws when magnitude of vector is 0.</exception>
    public Vector3 GetNormalized()
    {
        var len = Magnitude();
        if (len == 0) throw new DivideByZeroException();
        return this / len;
    }

    /// <summary>
    ///     Calculates cross product on this and <paramref name="other" /> vector and returns the results.
    /// </summary>
    /// <param name="other">The right operand of the cross.</param>
    /// <returns>Vector resulting from cross.</returns>
    public Vector3 Cross(Vector3 other)
    {
        return new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z,
            X * other.Y - Y * other.X);
    }

    /// <summary>
    ///     Calculates cross product on two vectors and returns the results.
    /// </summary>
    /// <param name="first">The left operand of the cross.</param>
    /// <param name="second">The right operand of the cross.</param>
    /// <returns>Vector resulting from cross.</returns>
    public static Vector3 Cross(Vector3 first, Vector3 second)
    {
        return new Vector3(first).Cross(second);
    }

    /// <summary>
    ///     Inverse vector.
    /// </summary>
    /// <returns>Inverted vector.</returns>
    public Vector3 Inverse()
    {
        return -this;
    }

    /// <summary>
    ///     Calculates Euclidean distance between this and other vector and returns the results.
    /// </summary>
    /// <param name="other">The second vector</param>
    /// <returns>Euclidean distance between vectors</returns>
    public double Distance(Vector3 other)
    {
        return Math.Sqrt(
            Math.Pow(X - other.X, 2) +
            Math.Pow(Y - other.Y, 2) +
            Math.Pow(Z - other.Z, 2)
        );
    }

    /// <summary>
    ///     Divides two vectors and returns the results.
    /// </summary>
    /// <param name="first">The left operand of the division.</param>
    /// <param name="second">The right operand of the division.</param>
    /// <returns>Division of two vectors.</returns>
    /// <exception cref="DivideByZeroException">
    ///     Thrown when at least one of the co-ordinates of <paramref name="second" /> are
    ///     equals 0.
    /// </exception>
    public static Vector3 operator /(Vector3 first, Vector3 second)
    {
        if (second.X == 0 || second.Y == 0 || second.Z == 0) throw new DivideByZeroException();
        return new Vector3(first.X / second.X, first.Y / second.Y, first.Z / second.Z);
    }
}