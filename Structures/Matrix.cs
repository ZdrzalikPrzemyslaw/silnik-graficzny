namespace Structures;

// https://codereview.stackexchange.com/questions/194732/class-matrix-implementation
public class Matrix : IEquatable<Matrix>
{
    private const double Eps = 1E-7;
    private double[,] _values;


    public Matrix(int rowCount, bool diagonal = false)
    {
        _values = new double[rowCount, rowCount];
        RowCount = rowCount;
        ColumnCount = rowCount;
        if (!diagonal) return;

        for (var i = 0; i < rowCount; i++) _values[i, i] = 1.0;
    }

    public Matrix(int rowCount, int columnCount)
    {
        _values = new double[rowCount, columnCount];
        RowCount = rowCount;
        ColumnCount = columnCount;
    }

    public Matrix(double[,] values)
    {
        _values = values;
        RowCount = values.GetLength(0);
        ColumnCount = values.GetLength(1);
    }

    public int RowCount { get; private set; }
    public int ColumnCount { get; private set; }

    public ref double this[int row, int column] => ref _values[row, column];

    public bool Equals(Matrix? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this == other;
    }

    // public static Matrix operator *(Matrix lhs, Matrix rhs)
    // {
    //     if (lhs.M != rhs.N) throw new MismatchedMatrixException();
    //     var c = new Matrix(lhs.M, rhs.N);
    //     // var c = new Matrix(lhs.N, rhs.M);
    //     for (var i = 0; i < c.N; i++)
    //     for (var j = 0; j < c.M; j++)
    //     {
    //         var temp = 0.0;
    //         for (var m = 0; m < lhs.N; m++) temp += lhs[i, m] * rhs[m, j];
    //         // for (var m = 0; m < lhs.M; m++) temp += lhs[i, m] * rhs[m, j];
    //         c[j, i] = temp;
    //     }
    //
    //     return c;
    // }


    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.ColumnCount != b.RowCount) throw new MismatchedMatrixException();
        var c = new Matrix(a.RowCount, b.ColumnCount);
        for (var i = 0; i < c.RowCount; i++)
        for (var j = 0; j < c.ColumnCount; j++)
        {
            var s = 0.0;
            for (var m = 0; m < a.ColumnCount; m++) s += a[i, m] * b[m, j];
            c[i, j] = s;
        }

        return c;
    }

    public static Matrix operator *(double lhs, Matrix rhs)
    {
        var matrix = new Matrix(rhs._values);
        for (var i = 0; i < rhs.RowCount; i++)
        for (var j = 0; j < rhs.ColumnCount; j++)
            matrix[i, j] *= lhs;

        return matrix;
    }

    public static Matrix operator *(Matrix lhs, double rhs)
    {
        return rhs * lhs;
    }

    public static Matrix operator +(Matrix lhs, Matrix rhs)
    {
        if (lhs.ColumnCount != rhs.ColumnCount || lhs.RowCount != rhs.RowCount) throw new MismatchedMatrixException();

        var ret = new Matrix(lhs.RowCount, lhs.ColumnCount);
        for (var i = 0; i < lhs.RowCount; i++)
        for (var j = 0; j < lhs.ColumnCount; j++)
            ret[i, j] = lhs[i, j] + rhs[i, j];

        return ret;
    }

    // todo: sprawdzic XD 
    public void Transpose()
    {
        var newValues = new double[ColumnCount, RowCount];
        for (var i = 0; i < RowCount; i++)
        for (var j = 0; j < ColumnCount; j++)
            newValues[j, i] = _values[i, j];

        _values = newValues;
        var tmp = RowCount;
        ColumnCount = RowCount;
        RowCount = tmp;
    }

    // todo: sprawdzic XD 
    public void Transpose(out Matrix m)
    {
        m = new Matrix(ColumnCount, RowCount);
        for (var i = 0; i < RowCount; i++)
        for (var j = 0; j < ColumnCount; j++)
            m[j, i] = _values[i, j];
    }

    public void SwapRows(int row1, int row2)
    {
        if (row1 > ColumnCount - 1 || row2 > ColumnCount - 1) throw new ArgumentException();
        for (var j = 0; j < RowCount; j++)
        {
            var tmp = _values[row1, j];
            _values[row2, j] = _values[row1, j];
            _values[row1, j] = tmp;
        }
    }

    public static bool operator ==(Matrix lhs, Matrix rhs)
    {
        if (lhs.RowCount != rhs.RowCount ||
            lhs.ColumnCount != rhs.ColumnCount)
            return false;

        for (var i = 0; i < lhs.RowCount; i++)
        for (var j = 0; j < lhs.ColumnCount; j++)
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
        for (var i = 0; i < RowCount; i++)
        for (var j = 0; j < ColumnCount; j++)
            hash.Add(_values[j, i]);
        return hash.ToHashCode();
    }

    public class MismatchedMatrixException : ArgumentException
    {
    }
}