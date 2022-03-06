namespace Structures;

public class Vector3
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

    public static Vector3 Zero()
    {
        return new Vector3();
    }

    public override string ToString()
    {
        return $"Vector({X}, {Y}, {Z})";
    }

    protected bool Equals(Vector3 other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Vector3) obj);
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
        return new Vector3(-a.X, -a.Y, -a.Z);
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

    public static bool operator ==(Vector3 a, Vector3 b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Vector3 a, Vector3 b)
    {
        return !(a == b);
    }

    public double Dot(Vector3 other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    public Vector3 GetNormalized()
    {
        var len = Magnitude();
        if (len == 0) throw new DivideByZeroException();
        return this / len;
    }

    public Vector3 Cross(Vector3 other)
    {
        return new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z,
            X * other.Y - Y * other.X);
    }

    public static Vector3 Cross(Vector3 first, Vector3 second)
    {
        return new Vector3(first).Cross(second);
    }

    public double Distance(Vector3 other)
    {
        return Math.Sqrt(
            Math.Pow(X - other.X, 2) +
            Math.Pow(Y - other.Y, 2) +
            Math.Pow(Z - other.Z, 2)
        );
    }

    public static Vector3 operator /(Vector3 first, Vector3 second)
    {
        if (second.X == 0 || second.Y == 0 || second.Z == 0) throw new DivideByZeroException();
        return new Vector3(first.X / second.X, first.Y / second.Y, first.Z / second.Z);
    }
}