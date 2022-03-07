using Structures;

namespace Zad1;

internal class Zad2
{
    public static void Main(string[] args)
    {
        var orthogonalCamera = new OrthogonalCamera(Vector3.Zero(), new Vector3(1, 0, 0));
        orthogonalCamera.RenderScene();
    }
}