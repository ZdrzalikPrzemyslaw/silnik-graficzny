using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;
using Structures.Surface;

namespace Zad3;

internal class Zad3
{
    public static void Main(string[] args)
    {
        ICamera cameraOrth = new OrthogonalCamera(new Vector3(1, 2, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        ICamera cameraPersp =
            new PerspectiveCamera(new Vector3(0, -1, -6),
                Vector3.Forward(),
                Vector3.Up());
        LightIntensity[,] tab = {};
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tab[i, j] = new LightIntensity(0.2, 0.5, 0.7);
            }
        }
        var scene = new Scene(
            new PlaneSlice(Vector3.Right(), new Vector3(-5, -5, 0), new Vector3(-5, -5, 0), new Vector3(-5, 5, 10),
                new Vector3(-5, -5, 10), new Vector3(-5, -5, 0), new Material(new Texture(10, 10, tab)))
            // new PlaneSlice(Vector3.Left(), new Vector3(5, -5, 0), new Vector3(5, -5, 0), new Vector3(5, 5, 10),
            //     new Vector3(5, -5, 10), new Vector3(5, -5, 0)),
            // new PlaneSlice(Vector3.Up(), new Vector3(-5, -5, 10), new Vector3(-5, -5, 10), new Vector3(5, -5, 10),
            //     new Vector3(5, -5, 0), new Vector3(-5, -5, 0)),
            // new PlaneSlice(Vector3.Back(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10), new Vector3(5, 5, 10),
            //     new Vector3(5, -5, 10), new Vector3(-5, -5, 10)),
            // new Sphere(
            //     new Vector3(-2, -3, 7), 2),
            // new Sphere(
            //     new Vector3(2, -3, 4), 2)
        );
        // scene.AddFigure(new OBJFileParser().ParseFile(new Vector3(0, 0, 0), "./cube.obj"));
        scene.AddLight(new AmbientLightSource(new LightIntensity(0.7, 0.7, 0.7)));
        // scene.AddLight(new PointLightSource(new LightIntensity(0.2, 1, 0.8), new Vector3(-5, -5, 5), 1, 1));
        // scene.AddLight(new PointLightSource(new LightIntensity(0.8, 0.2, 0.2), new Vector3(0, -10, 0), 1, 1));
        // scene.AddLight(new PointLightSource(new LightIntensity(0.8, 0.2, 0.4), new Vector3(0, 0, -10), 1, 1));
        // var pointLight = new PointLightSource(new LightIntensity(0.3, 0.7, 0.33), new Vector3(-4, 5, 9), 1, 1);
        // var pointLight2 = new PointLightSource(new LightIntensity(0.8, 0.1, 0.77), new Vector3(4, 5, 9), 1, 1);
        // var plane = new Plane(Vector3.Down(), 10);
        // scene.AddLight(pointLight);
        // scene.AddLight(pointLight2);
        // scene.AddLight(new SurfaceLightSource(pointLight, plane, Vector3.Back(), 2, 2, 4, 4).GetLightSources());

        var picture1 = cameraOrth.RenderScene(scene);
        var picture2 = cameraPersp.RenderScene(scene);

        picture1.PrintToPath("./", "PicOrth.png");
        picture2.PrintToPath("./", "PicPersp.png");
    }
}