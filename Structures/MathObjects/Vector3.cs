namespace Structures.MathObjects;

public class Vector3 : IEquatable<Vector3>
{
    /// <summary>
    ///     Creates new vector with coordinates as (<paramref name="x" />, <paramref name="y" />, <paramref name="z" />).
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">X coordinate.</param>
    /// <param name="z">X coordinate.</param>
    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    ///     Creates new vector between two vectors.
    /// </summary>
    /// <param name="vec1">Origin</param>
    /// <param name="vec2">Destination</param>
    public Vector3(Vector3 vec1, Vector3 vec2)
    {
        X = vec2.X - vec1.X;
        Y = vec2.Y - vec1.Y;
        Z = vec2.Z - vec1.Z;
    }

    /// <summary>
    ///     Creates new vector with the same coordinates as <paramref name="vector3" />.
    /// </summary>
    /// <param name="vector3">Vector to be copied.</param>
    public Vector3(Vector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }

    /// <summary>
    ///     Creates new vector with coordinates (0, 0, 0).
    /// </summary>
    public Vector3()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    /// <summary>
    ///     X coordinate of vector.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    ///     Y coordinate of vector.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    ///     Z coordinate of vector.
    /// </summary>
    public double Z { get; set; }

    /// <inheritdoc />
    public bool Equals(Vector3? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }

    /// <summary>
    ///     Creates new vector with coordinates (0, 0, 0) and returns the results.
    /// </summary>
    /// <returns>The instance of new zero vector.</returns>
    public static Vector3 Zero()
    {
        return new Vector3();
    }

    public static Vector3 Back()
    {
        return new Vector3(0, 0, -1);
    }

    public static Vector3 Forward()
    {
        return new Vector3(0, 0, 1);
    }

    public static Vector3 Left()
    {
        return new Vector3(-1, 0, 0);
    }

    public static Vector3 Right()
    {
        return new Vector3(1, 0, 0);
    }

    public static Vector3 Up()
    {
        return new Vector3(0, 1, 0);
    }

    public static Vector3 Down()
    {
        return new Vector3(0, -1, 0);
    }


    /// <inheritdoc />
    public override string ToString()
    {
        return $"Vector({X}, {Y}, {Z})";
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Vector3);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    /// <summary>
    ///     Calculates the magnitude of this adn return the results.
    /// </summary>
    /// <returns>Magnitude of this.</returns>
    public double Magnitude()
    {
        return System.Math.Sqrt(MagnitudeSquared());
    }

    /// <summary>
    ///     Calculates the squared magnitude of this adn return the results.
    /// </summary>
    /// <returns>Squared magnitude of this.</returns>
    public double MagnitudeSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    /// <summary>
    ///     Returns the new instance of <paramref name="a" />.
    /// </summary>
    /// <param name="a">Vector from which we got new instances.</param>
    /// <returns>Returns the new instance of param <paramref name="a" />.</returns>
    public static Vector3 operator +(Vector3 a)
    {
        return new Vector3(a);
    }

    /// <summary>
    ///     Inverse vector.
    /// </summary>
    /// <param name="a">Vector to inverse.</param>
    /// <returns>Inverted vector.</returns>
    public static Vector3 operator -(Vector3 a)
    {
        return a * -1;
    }

    /// <summary>
    ///     Multiplies the vector and a constant.
    /// </summary>
    /// <param name="a">A vector to be multiplied.</param>
    /// <param name="k">The constant to be multiplied.</param>
    /// <returns>Multiplication of the vector and a constant.</returns>
    public static Vector3 operator *(Vector3 a, double k)
    {
        return new Vector3(a.X * k, a.Y * k, a.Z * k);
    }

    /// <summary>
    ///     Multiplies a constant and the vector.
    /// </summary>
    /// <param name="k">The constant to be multiplied.</param>
    /// <param name="a">A vector to be multiplied.</param>
    /// <returns>Multiplication of the vector and a constant.</returns>
    public static Vector3 operator *(double k, Vector3 a)
    {
        return new Vector3(a.X * k, a.Y * k, a.Z * k);
    }

    /// <summary>
    ///     Divides a vector by a constant and returns the results.
    /// </summary>
    /// <param name="first">The vector which is a denominator.</param>
    /// <param name="k">A constant which is a divisor.</param>
    /// <returns>Division of vector by a constant.</returns>
    /// <exception cref="DivideByZeroException">
    ///     Thrown when <paramref name="k" /> is equals 0.
    /// </exception>
    public static Vector3 operator /(Vector3 first, double k)
    {
        if (k == 0) throw new DivideByZeroException();
        return first * (1 / k);
    }

    /// <summary>
    ///     Adds two vectors and returns the results.
    /// </summary>
    /// <param name="a">The left operand of the addition.</param>
    /// <param name="b">The right operand of the addition.</param>
    /// <returns>Sum of two vectors.</returns>
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <summary>
    ///     Subtracts two vectors and returns the results.
    /// </summary>
    /// <param name="a">The left operand of the subtraction.</param>
    /// <param name="b">The right operand of the subtraction.</param>
    /// <returns>Difference of two vectors.</returns>
    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return a + -b;
    }

    /// <summary>
    ///     Multiplies two vectors and returns the results.
    /// </summary>
    /// <param name="a">The left operand of the multiplication.</param>
    /// <param name="b">The right operand of the multiplication.</param>
    /// <returns>Multiplication of two vectors.</returns>
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
        return System.Math.Sqrt(
            System.Math.Pow(X - other.X, 2) +
            System.Math.Pow(Y - other.Y, 2) +
            System.Math.Pow(Z - other.Z, 2)
        );
    }


    /// <summary>
    ///     Calculates the distance between this and a ray's origin and return the results.
    /// </summary>
    /// <param name="ray">Ray to calculate the distance.</param>
    /// <returns>The distance between this and a ray's origin.</returns>
    public double Distance(Ray ray)
    {
        return Distance(ray.Origin);
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

    public Matrix AsMatrix()
    {
        return new Matrix(
            new[,]
            {
                { X },
                { Y },
                { Z }
            });
    }

    public Matrix AsMatrix4x1()
    {
        return new Matrix(
            new[,]
            {
                { X },
                { Y },
                { Z },
                { 1 }
            });
    }

    public static Vector3 FromMatrix(Matrix matrix)
    {
        if (matrix.ColumnCount != 1 || matrix.RowCount < 3) throw new Matrix.MismatchedMatrixException();

        return new Vector3(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
    }

    public Vector3 Rotate(Matrix matrix)
    {
        return FromMatrix(matrix * AsMatrix4x1());
    }

    // https://math.stackexchange.com/questions/2093314/rotation-matrix-of-rotation-around-a-point-other-than-the-origin
    public Vector3 Rotate(Matrix matrix, Vector3 pointOfRotation)
    {
        var translation1 = new Matrix(4, true);
        translation1[0, 3] = pointOfRotation.X;
        translation1[1, 3] = pointOfRotation.Y;
        translation1[2, 3] = pointOfRotation.Z;
        var translation2 = new Matrix(4, true);
        translation2[0, 3] = -pointOfRotation.X;
        translation2[1, 3] = -pointOfRotation.Y;
        translation2[2, 3] = -pointOfRotation.Z;
        var translationMatrix = translation1 * matrix * translation2;
        return Rotate(translationMatrix);
    }
}