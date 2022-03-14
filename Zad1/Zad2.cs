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
        ICamera perspectiveCamera = new PerspectiveCamera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        // ICamera perspectiveCamera = new PerspectiveCamera(new Vector3(15, 0, 10), new Vector3(-1, 0, 0), new Vector3(0, 1, 0));
        perspectiveCamera.RenderScene(
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
                        .Build()),
                new Sphere(
                    new Vector3(5, 5, 5), 2,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0.3)
                        .SetG(0.5)
                        .SetB(0.6)
                        .Build()),
                new Sphere(
                    new Vector3(5, -5, 5), 2,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0.7)
                        .SetG(0.8)
                        .SetB(0.1)
                        .Build()),
                new Sphere(
                    new Vector3(-5, 5, 5), 2,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0.4)
                        .SetG(0.4)
                        .SetB(0.4)
                        .Build()),
                new Sphere(
                    new Vector3(-5, -5, 5), 2,
                    new LightIntensity.LightIntensityBuilder()
                        .SetR(0.98)
                        .SetG(0.23)
                        .SetB(0.67)
                        .Build())
                ));
    }
}