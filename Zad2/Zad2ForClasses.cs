﻿using Structures.Figures;
using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Camera;

namespace Zad2;

internal class Zad2ForClasses
{
    public static void Main(string[] args)
    {
        var matrix = Matrix.RotateZ(90 * Math.PI / 180);
        var matrix2 = Matrix.Rotate(90 * Math.PI / 180, new Vector3(0, 0, 1));
        ICamera camera = new OrthogonalCamera(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        // ICamera camera =
        //     new PerspectiveCamera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        // ICamera perspectiveCamera = new PerspectiveCamera(new Vector3(15, 0, 10), new Vector3(-1, 0, 0), new Vector3(0, 1, 0));
        var scene = new Scene(
            new Sphere(
                new Vector3(0, 0, 4), 0.5,
                new LightIntensity.LightIntensityBuilder()
                    .SetR(0)
                    .SetG(0)
                    .SetB(1)
                    .Build()),
            new Sphere(
                new Vector3(0.5, 0, 5), 0.5,
                new LightIntensity.LightIntensityBuilder()
                    .SetR(1)
                    .SetG(0)
                    .SetB(0)
                    .Build())
        );
        for (var i = 0; i < 6; i++)
        for (var j = 0; j < 6; j++)
            scene.AddFigure(new PlaneSlice(
                new Vector3(0, 0, -1),
                new Vector3(-3, 3, 5),
                new LightIntensity(i / 5.0, j / 5.0, i / 10.0 + j / 10.0),
                new Vector3(-3 + i, 3 - j, 5),
                new Vector3(-2 + i, 3 - j, 5),
                new Vector3(-2 + i, 2 - j, 5),
                new Vector3(-3 + i, 2 - j, 5)
            ));
        var picture = camera.RenderScene(scene);
        picture.PrintToPath();
    }
}