using Structures;
using Structures.Camera;

namespace Zad1;

internal class Zad2
{
    public static void Main(string[] args)
    {
        var vector3 = new Vector3(1, 0, 0);
        var matrix = new Matrix(new double[4, 4]
        {
            { 0, -1, 0, 0 },
            { 1, 0, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
        });
        var res = vector3.Rotate(matrix);
        ICamera orthogonalCamera = new OrthogonalCamera(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        orthogonalCamera.RenderScene(new Scene(new Sphere(new Vector3(0, 0, 5), 1)));
    }
}