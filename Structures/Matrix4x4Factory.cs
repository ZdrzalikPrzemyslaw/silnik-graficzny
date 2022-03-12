namespace Structures;

public class Matrix4x4Factory : FactoryPattern<Matrix4x4>
{
    private readonly double[,] _values;

    public Matrix4x4Factory(double[,] values)
    {
        if (values.GetLength(0) != 4 || values.GetLength(1) != 4) throw new Matrix.MismatchedMatrixException();
        _values = values;
    }

    public Matrix4x4 create()
    {
        return new Matrix4x4(_values);
    }
}