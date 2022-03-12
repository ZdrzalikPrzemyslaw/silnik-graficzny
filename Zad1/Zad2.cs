using Structures;
using Structures.Camera;

namespace Zad1;

internal class Zad2
{
    public static void Main(string[] args)
    {
        var matrix = Matrix.RotateZ(90 * Math.PI / 180);
        var matrix2 = Matrix.Rotate(90 * Math.PI / 180, new Vector3(0, 0, 1));
        // ICamera orthogonalCamera = new OrthogonalCamera(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        // orthogonalCamera.RenderScene(new Scene(new Sphere(new Vector3(0, 0, 5), 1)));
        ICamera orthogonalCamera = new PerspectiveCamera(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        orthogonalCamera.RenderScene(
            new Scene(
                new Sphere(
                    new Vector3(0, 0, 10), 3,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0)
                        .SetG(0)
                        .SetB(1)
                        .Build()),
                new Sphere(
                    new Vector3(5, 0, 20), 5,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(1)
                        .SetG(0)
                        .SetB(0)
                        .Build()),
                new Sphere(
                    new Vector3(0, -5, 15), 4,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0)
                        .SetG(1)
                        .SetB(0)
                        .Build())));
    }
}