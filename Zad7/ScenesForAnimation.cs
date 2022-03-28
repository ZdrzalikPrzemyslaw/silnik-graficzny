using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;
using Structures.Surface;
using Structures.Surface.Reflection;

namespace Zad7;

public class ScenesForAnimation
{
    public PlaneSlice? WallLeft { get; set; }
    public PlaneSlice? WallRight { get; set; }
    public PlaneSlice? WallBack { get; set; }
    public PlaneSlice? WallDown { get; set; }
    public PlaneSlice? WallUp { get; set; }
    public Sphere? Sphere { get; set; }
    public SurfaceLightSource? SurfaceLightSource { get; set; }
    public AmbientLightSource? AmbientLightSource { get; set; }

    public List<SceneAndCamera> CreateScenesForAnimations(int fps)
    {
        InitFigures();
        List<SceneAndCamera> sceneAndCameras = new List<SceneAndCamera>();
        double a = 9.8;
        double x = -1.5;
        double y = 15;
        double z = 8;
        double v = 0.0;
        for(int i = 0; i < fps * 2.5; i++)
        {
            sceneAndCameras.Add(CreateOneSceneAndAnimation(x, y, z));
            v += a / fps;
            y -= v / fps;
            if (y <= -3.5)
            {
                y = -3.5;
                v *= -0.1;
            }
        }
        return sceneAndCameras;
    }

    private void InitFigures()
    {
        var materialWhite = new Material(new Texture(new LightIntensity[,]
            {
                {new(1, 1, 1)}
            }
        ));

        var tab = new LightIntensity[10, 10];
        for (var i = 0; i < 10; i++)
        {
            var random = new Random();
            var r = random.NextDouble();
            var g = random.NextDouble();
            var b = random.NextDouble();
            for (var j = 0; j < 10; j++) tab[i, j] = new LightIntensity(r, g, b);
        }

        var materialColor = new Material(new Texture(tab));
        WallLeft = new PlaneSlice(Vector3.Right(), new Vector3(-5, 5, 0), new Vector3(-5, 5, 0),
            new Vector3(-5, 5, 10),
            new Vector3(-5, -5, 10), new Vector3(-5, -5, 0), materialColor);

        WallRight = new PlaneSlice(Vector3.Left(), new Vector3(5, 5, 10), new Vector3(5, 5, 10),
            new Vector3(5, 5, 0),
            new Vector3(5, -5, 0), new Vector3(5, -5, 10), materialColor);

        WallBack = new PlaneSlice(Vector3.Back(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10),
            new Vector3(5, 5, 10), new Vector3(5, -5, 10), new Vector3(-5, -5, 10), materialColor);

        WallDown = new PlaneSlice(Vector3.Up(), new Vector3(-5, -5, 10), new Vector3(-5, -5, 10),
            new Vector3(5, -5, 10),
            new Vector3(5, -5, 0), new Vector3(-5, -5, 0), materialColor);

        WallUp = new PlaneSlice(Vector3.Down(), new Vector3(-1, 5, 7), new Vector3(-1, 5, 6),
            new Vector3(1, 5, 6), new Vector3(1, 5, 7), new Vector3(-1, 5, 7), materialWhite);
        Sphere = new Sphere(new Vector3(1.5, -3.5, 5), 1.5, new Material(null, new RefractiveReflection(1.52)));
        var PointLightSource = new PointLightSource(new LightIntensity(1, 1, 1), new Vector3(-1, 3, 7), 1, 1);
        var plane = new Plane(Vector3.Down(), 10);
        SurfaceLightSource = new SurfaceLightSource(PointLightSource, plane, Vector3.Forward(), 2, 1, 4, 4);
        AmbientLightSource = new AmbientLightSource(new LightIntensity(0.2, 0.2, 0.2));
    }


    private SceneAndCamera CreateOneSceneAndAnimation(double x, double y, double z)
    {
        ICamera cameraPersp = new PerspectiveCamera(new Vector3(0, -1, -4), Vector3.Forward(), Vector3.Up());
        // -1.5, -3.5, 8
        var sphere2 = new Sphere(new Vector3(x, y, z), 1.5, new Material(null, new SpecularReflection()));
        
        var scene = new Scene(WallLeft, WallRight, WallBack, WallDown, WallUp, Sphere, sphere2);
        scene.AddLight(SurfaceLightSource.GetLightSources());
        scene.AddLight(AmbientLightSource);
        return new SceneAndCamera(scene, cameraPersp);
    }
}