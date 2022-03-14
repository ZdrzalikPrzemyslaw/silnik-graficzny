using System.Diagnostics.CodeAnalysis;
using SkiaSharp;

namespace Structures.Camera;

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
    public override void RenderScene(Scene scene)
    {
        // //Left down
        // var leftDownMatrix = Matrix.RotateY(-Fov / 2 * Math.PI / 180) * Matrix.RotateX(-Fov / 2 * Math.PI / 180);
        // var rayLeftDown = new Ray(Position, Target).Rotate(leftDownMatrix);
        // var leftDownPoint = NearPlane.Intersection(rayLeftDown);
        //
        // //Left Up
        // var leftUpMatrix = Matrix.RotateY(Fov / 2 * Math.PI / 180) * Matrix.RotateX(-Fov / 2 * Math.PI / 180);
        // var rayLeftUp = new Ray(Position, Target).Rotate(leftUpMatrix);
        // var leftUpPoint = NearPlane.Intersection(rayLeftUp);
        //
        // //Right Down
        // var rightDownMatrix = Matrix.RotateY(-Fov / 2 * Math.PI / 180) * Matrix.RotateX(Fov / 2 * Math.PI / 180);
        // var rightDownRay = new Ray(Position, Target).Rotate(rightDownMatrix);
        // var rightUDownPoint = NearPlane.Intersection(rightDownRay);
        //
        // //Right Down
        // var rightUpMatrix = Matrix.RotateY(Fov / 2 * Math.PI / 180) * Matrix.RotateX(Fov / 2 * Math.PI / 180);
        // var rightUpRay = new Ray(Position, Target).Rotate(rightUpMatrix);
        // var rightUpPoint = NearPlane.Intersection(rightUpRay);


        Picture picture = new(800, 800);
        var pixelWidth = Fov / picture.Bitmap.Width;
        var pixelHeight = Fov / picture.Bitmap.Height;
        var startX = -Fov / 2;
        var startY = Fov / 2;
        Matrix matrix = null;
        Matrix matrix2 = null;
        Ray ray = null;
        Ray ray2 = null;
        for (var i = 0; i < picture.Bitmap.Width; i++)
        for (var j = 0; j < picture.Bitmap.Height; j++)
        {
            var locX = startX + i * pixelWidth;
            var locY = startY - j * pixelHeight;

            matrix = Matrix.Rotate(locY * Math.PI / 180, Up) *
                     Matrix.Rotate(locX * Math.PI / 180, Up.Cross(Target));
            ray = new Ray(Position, Target).Rotate(matrix);

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

        using (var data = picture.Bitmap.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(Path.Combine("./", "Picture.png")))
        {
            data.SaveTo(stream);
        }
    }
}