namespace Structures;

public class Vector3
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public double GetLength()
    {
        return Math.Sqrt(GetLengthSquared());
    }

    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3(Vector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }

    public double GetLengthSquared()
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

    public static Vector3 operator /(Vector3 a, double k)
    {
        if (k == 0) throw new DivideByZeroException();
        return new Vector3(a.X / k, a.Y / k, a.Z / k);
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return a + -b;
    }

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    // public static Vector3 operator /(Vector3 a, Vector3 b)
    // {
    //     if (b.X == 0 || b.Y == 0 || b.Z == 0)
    //     {
    //         throw new DivideByZeroException();
    //     }
    //     return new Vector3(a.num * b.den, a.den * b.num);
    // }
}