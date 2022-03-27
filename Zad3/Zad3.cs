using System.Diagnostics;
using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;
using Structures.Render.Light;
using Structures.Surface;
using Structures.Surface.Reflection;

namespace Zad3;

internal class Zad3
{
    public static void Main(string[] args)
    {
        // ICamera cameraOrth = new OrthogonalCamera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        ICamera cameraPersp =
            new PerspectiveCamera(new Vector3(0, 0, 0),
                Vector3.Forward(),
                Vector3.Up());
        var tab = new LightIntensity[10, 10];
        for (var i = 0; i < 10; i++)
        {
            var random = new Random();
            var r = random.NextDouble();
            var g = random.NextDouble();
            var b = random.NextDouble();
            for (var j = 0; j < 10; j++) tab[i, j] = new LightIntensity(r, g, b);
        }
        
        var tab3 = new LightIntensity[10, 10];
        for (var i = 0; i < 10; i++)
        {
            var r = 1;
            var g = 1;
            var b = 1;
            for (var j = 0; j < 10; j++) tab3[i, j] = new LightIntensity(r, g, b);
        }
        
        for (var i = 0; i < 10; i++)
        {
            var r = 1;
            var g = 0;
            var b = 0;
            tab3[i, i] = new LightIntensity(r, g, b);
            tab3[9 - i, 9 - i] = new LightIntensity(r, g, b);
        }
        

        var tab2 = new LightIntensity[50, 10];
        for (var i = 0; i < 50; i++)
        {
            var random = new Random();
            var r = 1;
            var g = 1;
            var b = 1;
            for (var j = 0; j < 10; j++) tab2[i, j] = new LightIntensity(r, g, b);
        }

        var sphere2 = new Sphere(
            new Vector3(0, 0, 5), 1, new Material(new Texture(tab2), new SpecularReflection()));
        sphere2.Rotation = Matrix.RotateX(Math.PI / 4);
        var scene = new Scene(
            new PlaneSlice(Vector3.Right(), new Vector3(-5, 5, 0), new Vector3(-5, 5, 0), new Vector3(-5, 5, 10),
                new Vector3(-5, -5, 10), new Vector3(-5, -5, 0), new Material(new Texture(tab3))),
            new PlaneSlice(Vector3.Left(), new Vector3(5, 5, 10), new Vector3(5, 5, 10), new Vector3(5, 5, 0),
                new Vector3(5, -5, 0), new Vector3(5, -5, 10), new Material(new Texture(tab))),
            new PlaneSlice(Vector3.Up(), new Vector3(-5, -5, 10), new Vector3(-5, -5, 10), new Vector3(5, -5, 10),
                new Vector3(5, -5, 0), new Vector3(-5, -5, 0), new Material(new Texture(tab))),
            new PlaneSlice(Vector3.Back(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10), new Vector3(0, 5, 10),
                new Vector3(0, -5, 10), new Vector3(-5, -5, 10), new Material(new Texture(tab), new SpecularReflection())),
            new PlaneSlice(Vector3.Back(), new Vector3(0, 5, 10), new Vector3(0, 5, 10), new Vector3(5, 5, 10),
                new Vector3(5, -5, 10), new Vector3(0, -5, 10), new Material(new Texture(tab))),
            new PlaneSlice(Vector3.Forward(), new Vector3(-5, 5, -1), new Vector3(-5, 5, -1), new Vector3(5, 5, -1),
                new Vector3(5, -5, -1), new Vector3(-5, -5, -1), new Material(new Texture(tab))),
            new PlaneSlice(Vector3.Down(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10), new Vector3(5, 5, 10),
                new Vector3(5, 5, 0), new Vector3(-5, 5, 0), new Material(new Texture(tab))),
            // new Sphere(
            //     new Vector3(-2, -3, 7), 2, new Material(new Texture(tab2))),
            sphere2
        );

        var kolega = new PlaneSlice(Vector3.Back(), new Vector3(-5, 5, 10), new Vector3(-5, 5, 10),
            new Vector3(5, 5, 10),
            new Vector3(5, -5, 10), new Vector3(-5, -5, 10), new Material(new Texture(tab), new SpecularReflection()));
        
        // var scene = new Scene(
        //     new PlaneSlice(Vector3.Right(), new Vector3(-5, 5, 0), new Vector3(-5, 5, 0), new Vector3(-5, 5, 10),
        //         new Vector3(-5, -5, 10), new Vector3(-5, -5, 0), new Material(new Texture(tab))),
        //     kolega
        // );
        // scene.AddFigure(new OBJFileParser().ParseFile(new Vector3(0, 0, 0), "./cube.obj"));
        
        // scene.AddLight(new AmbientLightSource(new LightIntensity(0.2, 0.2, 0.2)));
        
        // scene.AddLight(new PointLightSource(new LightIntensity(0.2, 1, 0.8), new Vector3(-5, -5, 5), 1, 1));
        // scene.AddLight(new PointLightSource(new LightIntensity(0.8, 0.2, 0.2), new Vector3(0, -10, 0), 1, 1));
        // scene.AddLight(new PointLightSource(new LightIntensity(0.8, 0.2, 0.4), new Vector3(0, 0, -10), 1, 1));
        var pointLight = new PointLightSource(new LightIntensity(1, 1, 1), new Vector3(-4, 4, 8), 1, 1);
        var pointLight2 = new PointLightSource(new LightIntensity(1, 1, 1), new Vector3(4, 1, 1), 1, 1);
        scene.AddLight(pointLight);
        scene.AddLight(pointLight2);
        var plane = new Plane(Vector3.Down(), 10);
        // scene.AddLight(new SurfaceLightSource(pointLight, plane, Vector3.Forward(), 8, 8).GetLightSources());

        // var picture1 = cameraOrth.RenderScene(scene);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var picture2 = cameraPersp.RenderScene(scene, 1000, 1000);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        stopwatch = new Stopwatch();
        stopwatch.Start();
        // var picture3 = cameraOrth.RenderScene(scene);
        stopwatch.Stop();
        // Console.WriteLine(stopwatch.ElapsedMilliseconds);


        // picture1.PrintToPath("./", "PicOrth.png");
        picture2.PrintToPath("./", "PicPersp.png");
        // picture3.PrintToPath("./", "PicOrth.png");
    }
}