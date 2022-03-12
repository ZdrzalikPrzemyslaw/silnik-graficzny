namespace Structures;

public class Matrix : IEquatable<Matrix>
{
    private const double Eps = 1E-7;
    private readonly double[,] _values;


    public Matrix(int n, bool diagonal = false)
    {
        _values = new double[n, n];
        N = n;
        M = n;
        if (!diagonal) return;

        for (var i = 0; i < n; i++) _values[i, i] = 1.0;
    }

    public Matrix(int n, int m)
    {
        _values = new double[n, m];
        N = n;
        M = m;
    }

    public Matrix(double[,] values)
    {
        _values = values;
        N = values.GetLength(0);
        M = values.GetLength(1);
    }

    public int N { get; }
    public int M { get; }

    public ref double this[int row, int column] => ref _values[row, column];

    public bool Equals(Matrix? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this == other;
    }

    // Todo: throws
    public static Matrix operator +(Matrix lhs, Matrix rhs)
    {
        if (lhs.M != rhs.M || lhs.N != rhs.N) throw new MismatchedMatrixException();

        var ret = new Matrix(lhs.N, lhs.M);
        for (var i = 0; i < lhs.N; i++)
        for (var j = 0; j < lhs.M; j++)
            ret[i, j] = lhs[i, j] + rhs[i, j];

        return ret;
    }

    // todo: sprawdzic XD 
    public Matrix Transpose()
    {
        var m = new Matrix(M, N);
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            m[j, i] = _values[i, j];

        return m;
    }

    public static bool operator ==(Matrix lhs, Matrix rhs)
    {
        if (lhs.N != rhs.N ||
            lhs.M != rhs.M)
            return false;

        for (var i = 0; i < lhs.N; i++)
        for (var j = 0; j < lhs.M; j++)
            if (Math.Abs(lhs[i, j] - rhs[i, j]) > Eps)
                return false;

        return true;
    }

    public static bool operator !=(Matrix lhs, Matrix rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Matrix);
    }

    public override int GetHashCode()
    {
        return _values.GetHashCode();
    }

    public class MismatchedMatrixException : ArgumentException
    {
    }
}