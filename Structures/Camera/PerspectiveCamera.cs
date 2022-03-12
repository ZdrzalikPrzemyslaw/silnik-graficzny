using SkiaSharp;

namespace Structures.Camera;

public class PerspectiveCamera : AbstractCamera
{
    public PerspectiveCamera() : this(Vector3.Zero(), new Vector3(0, 0, 1), new Vector3(0, 1, 0))
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
        for (var i = 0; i < picture.Bitmap.Width; i++)
        for (var j = 0; j < picture.Bitmap.Height; j++)
        {
            var locX = startX + i * pixelWidth;
            var locY = startY - j * pixelHeight;

            var matrix = Matrix.RotateY(locY * Math.PI / 180) * Matrix.RotateX(locX * Math.PI / 180);
            var ray = new Ray(Position, Target).Rotate(matrix);

            Vector3? intersection = null;
            try
            {
                intersection = scene.Intersection(ray);
            }
            catch (Plane.InfiniteIntersectionsException)
            {
            }

            picture.SetPixel(i, j,
                intersection is not null ? new LightIntensity(1, 0.78, 0.64) : new LightIntensity(0.64, 0.67, 1));
        }

        using (var data = picture.Bitmap.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(Path.Combine("D:/", "Picture.png")))
        {
            data.SaveTo(stream);
        }
    }
}