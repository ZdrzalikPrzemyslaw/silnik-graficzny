using System.Diagnostics;
using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;
using Structures.Surface;
using Structures.Surface.Reflection;

namespace Zad6;

internal class Zad6
{
    public static void Main(string[] args)
    {
        ICamera cameraPersp = new PerspectiveCamera(new Vector3(0, -1, -4), Vector3.Forward(), Vector3.Up());
        var materialRed = new Material(new Texture(new LightIntensity[,]
            {
                { new(1, 0, 0) }
            }
        ));
        var materialBlue = new Material(new Texture(new LightIntensity[,]
            {
                { new(0, 0, 1) }
            }
        ));

        var materialGray = new Material(new Texture(new LightIntensity[,]
            {
                { new(0.3, 0.3, 0.3) }
            }
        ));

        var materialWhite = new Material(new Texture(new LightIntensity[,]
            {
                { new(1, 1, 1) }
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

        var wallLeft = new PlaneSlice(Vector3.Right(), new Vector3(-5, 5, 0), new Vector3(-5, 5, 0),
            new Vector3(-5, 5, 10),
            new Vector3(-5, -5, 10), new Vector3(-5, -5, 0), materialColor);

        var wallRight = new PlaneSlice(Vector3.Left(), new Vector3(5, 5, 10), new Vector3(5, 5, 10),
            new Vector3(5, 5, 0),
            new Vector3(5, -5, 0), new Vector3(5, -5, 10), materialColor);

        var wallBack = new PlaneSlice(Vector3.Back(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10),
            new Vector3(5, 5, 10), new Vector3(5, -5, 10), new Vector3(-5, -5, 10), materialColor);

        var wallDown = new PlaneSlice(Vector3.Up(), new Vector3(-5, -5, 10), new Vector3(-5, -5, 10),
            new Vector3(5, -5, 10),
            new Vector3(5, -5, 0), new Vector3(-5, -5, 0), materialColor);
        
        var wallUp = new PlaneSlice(Vector3.Down(), new Vector3(-1, 5, 7), new Vector3(-1, 5, 6),
            new Vector3(1, 5, 6), new Vector3(1, 5, 7), new Vector3(-1, 5, 7), materialWhite);


        var sphere = new Sphere(new Vector3(-1.5, -3.5, 8), 1.5, new Material(null, new SpecularReflection()));
        var sphere2 = new Sphere(new Vector3(1.5, -3.5, 5), 1.5, new Material(null, new RefractiveReflection(1.52)));
        

        var scene = new Scene(wallLeft, wallRight, wallBack, wallDown, wallUp, sphere, sphere2);
        
        
        // var pointLight = new PointLightSource(new LightIntensity(1, 1, 1), new Vector3(-1, 3, 8), 1, 1);
        var pointLight2 = new PointLightSource(new LightIntensity(1, 1, 1), new Vector3(-1, 3, 7), 1, 1);
        var plane = new Plane(Vector3.Down(), 10);
        // scene.AddLight(new SurfaceLightSource(pointLight, plane, Vector3.Forward(), 2, 1, 4, 4).GetLightSources());
        scene.AddLight(new SurfaceLightSource(pointLight2, plane, Vector3.Forward(), 2, 1, 4, 4).GetLightSources());
        scene.AddLight(new AmbientLightSource(new LightIntensity(0.2, 0.2, 0.2)));
        // scene.AddLight(pointLight);
        
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var picture2 = cameraPersp.RenderScene(scene, 512, 512);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        
        picture2.PrintToPath("./", "PicPersp.png");
    }
}