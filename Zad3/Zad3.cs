using Structures.Figures;
using Structures.MathObjects;
using Structures.OBJParser;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;

namespace Zad3;

internal class Zad3
{
    public static void Main(string[] args)
    {
        ICamera cameraOrth = new OrthogonalCamera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        ICamera cameraPersp =
            new PerspectiveCamera(new Vector3(0, 0, 0), new Vector3(0, 0, 1),
                new Vector3(0, 1, 0));
        var scene = new Scene(
            // new Sphere(
                // new Vector3(0, 0, 4), 1.1)
            // new Sphere(
            //     new Vector3(1.5, 0, 5), 0.6)
        );
        scene.AddFigure(new OBJFileParser().ParseFile(new Vector3(0, 0, 4), "./ostroslup.obj"));
        scene.AddLight(new AmbientLightSource(new LightIntensity(0.1, 0.1, 0.1)));
        scene.AddLight(new PointLightSource(new LightIntensity(1, 0, 0), new Vector3(-3, 0, 0), 1, 1));

        var picture1 = cameraOrth.RenderScene(scene);
        var picture2 = cameraPersp.RenderScene(scene);

        picture1.PrintToPath("./", "PicOrth.png");
        picture2.PrintToPath("./", "PicPersp.png");
    }
}