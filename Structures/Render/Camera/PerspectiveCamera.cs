using System.Diagnostics.CodeAnalysis;
using Structures.Figures;
using Structures.MathObjects;
using Structures.Render.Sampler;

namespace Structures.Render.Camera;

public class PerspectiveCamera : AbstractCamera
{
    public PerspectiveCamera() : this(Vector3.Zero(), Vector3.Forward(), Vector3.Up())
    {
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up, Plane nearPlane, Plane farPlane, double fov)
        : base(position, target, up)
    {
        NearPlane = nearPlane;
        FarPlane = farPlane;
        Fov = fov;
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up)
        : this(position, target, up,
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1)),
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1000)),
            90)
    {
    }

    public Plane NearPlane { get; set; }
    public Plane FarPlane { get; set; }
    public double Fov { get; set; }

    public ISampler Sampler { get; } = new PerspectiveSampler();
    
    public override Picture RenderScene(Scene scene)
    {
        Picture picture = new(1000, 1000);
        var pixelWidth = Fov / picture.Bitmap.Width;
        var pixelHeight = Fov / picture.Bitmap.Height;
        var startX = -Fov / 2;
        var startY = Fov / 2;
        Ray ray = new Ray(Position, Target);
        Matrix matrixX = null;
        Matrix matrixY = null;
        LightIntensity intersection = null;
        for (var i = 0; i < picture.Bitmap.Width; i++)
        {
            matrixX = Matrix.Rotate((startX + i * pixelWidth) * Math.PI / 180, Up);
            for (var j = 0; j < picture.Bitmap.Height; j++)
            {
                matrixY = Matrix.Rotate((startY + -j * pixelHeight) * Math.PI / 180, Target.Cross(Up));
                ray = new Ray(Position, Target).Rotate(matrixY * matrixX);

                intersection = Sampler.Sample(scene, ray, pixelWidth, Up);

                picture.SetPixel(i, j, intersection);
            }
        }

        return picture;
    }
}