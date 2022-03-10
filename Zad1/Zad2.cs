using Structures;
using Structures.Camera;

namespace Zad1;

internal class Zad2
{
    public static void Main(string[] args)
    {
        ICamera orthogonalCamera = new OrthogonalCamera(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        orthogonalCamera.RenderScene(new Scene(new Sphere(new Vector3(0, 0, 5), 1)));
    }
}