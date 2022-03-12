namespace Structures;

public class Matrix4x4 : Matrix
{
    public Matrix4x4(bool diagonal = false) : base(4, diagonal)
    {
        System.Numerics.Vector3
    }

    internal Matrix4x4(double[,] values) : base(values)
    {
        
    }
}