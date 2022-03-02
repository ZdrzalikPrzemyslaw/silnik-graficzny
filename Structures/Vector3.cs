﻿namespace Structures;

public class Vector3
{
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

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public double GetLength()
    {
        return Math.Sqrt(GetLengthSquared());
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
        var inverse = 1 / k;
        return new Vector3(a.X / inverse, a.Y / inverse, a.Z / inverse);
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return a + -b;
    }

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }

    public double Dot(Vector3 other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    public Vector3 GetNormalized()
    {
        var newVec = new Vector3(this);
        var len = newVec.GetLength();
        if (len == 0) throw new DivideByZeroException();

        newVec.X /= len;
        newVec.Y /= len;
        newVec.Z /= len;
        return newVec;
    }

    public Vector3 Cross(Vector3 other)
    {
        return new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z,
            X * other.Y - Y * other.X);
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