using System.Diagnostics.CodeAnalysis;
using Structures.Figures;
using Structures.MathObjects;

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

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Double[,]")]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Double[,]")]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
        MessageId = "type: System.Double[,]")]
    public override Picture RenderScene(Scene scene)
    {
        Picture picture = new(1000, 1000);
        var pixelWidth = Fov / picture.Bitmap.Width;
        var pixelHeight = Fov / picture.Bitmap.Height;
        var startX = -Fov / 2;
        var startY = Fov / 2;
        Matrix matrix = null;
        Ray ray = null;
        for (var i = 0; i < picture.Bitmap.Width; i++)
        for (var j = 0; j < picture.Bitmap.Height; j++)
        {
            var locX = startX + i * pixelWidth;
            var locY = startY - j * pixelHeight;

            matrix = Matrix.Rotate(locY * Math.PI / 180, Target.Cross(Up));
            var matrix2 = Matrix.Rotate(locX * Math.PI / 180, Up);
            ray = new Ray(Position, Target).Rotate(matrix * matrix2);

            Figure? intersection = null;
            try
            {
                intersection = scene.GetClosest(ray);
            }
            catch (Plane.InfiniteIntersectionsException)
            {
            }

            picture.SetPixel(i, j, intersection?.LightIntensity ?? new LightIntensity(0.64, 0.67, 1));
        }

        return picture;
    }
}