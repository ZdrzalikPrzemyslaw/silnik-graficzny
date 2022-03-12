namespace Structures;

// https://codereview.stackexchange.com/questions/194732/class-matrix-implementation
public class Matrix : IEquatable<Matrix>
{
    private const double Eps = 1E-7;
    private double[,] _values;


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

    public int N { get; private set; }
    public int M { get; private set; }

    public ref double this[int row, int column] => ref _values[row, column];

    public bool Equals(Matrix? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this == other;
    }

    public static Matrix operator *(Matrix lhs, Matrix rhs)
    {
        if (lhs.M != rhs.N) throw new MismatchedMatrixException();
        var c = new Matrix(lhs.M, rhs.N);
        for (var i = 0; i < c.N; i++)
        for (var j = 0; j < c.M; i++)
        {
            var temp = 0.0;
            for (var m = 0; m < lhs.N; m++) temp += lhs[i, m] * rhs[m, j];
            c[j, i] = temp;
        }

        return c;
    }

    public static Matrix operator *(double lhs, Matrix rhs)
    {
        var matrix = new Matrix(rhs._values);
        for (var i = 0; i < rhs.N; i++)
        for (var j = 0; j < rhs.M; j++)
            matrix[i, j] *= lhs;

        return matrix;
    }

    public static Matrix operator *(Matrix lhs, double rhs)
    {
        return rhs * lhs;
    }

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
    public void Transpose()
    {
        var newValues = new double[M, N];
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            newValues[j, i] = _values[i, j];

        _values = newValues;
        var tmp = N;
        M = N;
        N = tmp;
    }

    // todo: sprawdzic XD 
    public void Transpose(out Matrix m)
    {
        m = new Matrix(M, N);
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            m[j, i] = _values[i, j];
    }

    public void SwapRows(int row1, int row2)
    {
        if (row1 > M - 1 || row2 > M - 1) throw new ArgumentException();
        for (var j = 0; j < N; j++)
        {
            var tmp = _values[row1, j];
            _values[row2, j] = _values[row1, j];
            _values[row1, j] = tmp;
        }
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
        HashCode hash = default;
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            hash.Add(_values[j, i]);
        return hash.ToHashCode();
    }

    public class MismatchedMatrixException : ArgumentException
    {
    }
}