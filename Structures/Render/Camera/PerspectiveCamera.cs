using Structures.Figures;
using Structures.MathObjects;
using Structures.Render.Light;
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
        FovX = fov;
        FovY = fov;
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up)
        : this(position, target, up,
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1)),
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1000)),
            80)
    {
    }

    public Plane NearPlane { get; set; }
    public Plane FarPlane { get; set; }
    public double FovX { get; set; }
    public double FovY { get; set; }

    public ISampler Sampler { get; } = new PerspectiveSampler();

    private void RenderPiece(Picture picture, Scene scene, int fromX, int toX, int fromY, int toY)
    {
        var pixelWidth = FovX / picture.Bitmap.Width;
        var pixelHeight = FovY / picture.Bitmap.Height;
        var startX = -FovX / 2;
        var startY = FovY / 2;
        var ray = new Ray(Position, Target);
        Vector3 right = Target.Cross(Up);
        LightIntensity intersection = null;
        for (var i = fromX; i < toX; i++)
        {
            for (var j = fromY; j < toY; j++)
            {
                var pixelX = ((startX + (pixelWidth * i)) / FovX) * 2;
                var pixelY = ((startY - (pixelHeight * j)) / FovY) * 2;
                ray = new Ray(Position, (Target - right * pixelX + Up * pixelY).GetNormalized());

                intersection = Sampler.Sample(scene, ray, pixelWidth, pixelHeight, Up);

                picture.SetPixel(i, j, intersection);
            }
        }
    }

    public override Picture RenderScene(Scene scene, int sizeX, int sizeY)
    {
        Picture picture = new(sizeX, sizeY);
        // todo: zrobic to lepiej
        FovY = FovX * sizeY / sizeX;
        
        var threads = new List<Thread>();
        for (var i = 0; i < 4; i++)
        for (var j = 0; j < 4; j++)
        {
            var copyI = i;
            var copyJ = j;
            var thread = new Thread(() => RenderPiece(
                picture,
                scene,
                sizeX / 4 * copyI,
                sizeX / 4 * (copyI + 1),
                sizeY / 4 * copyJ,
                sizeY / 4 * (copyJ + 1)
            ));
            thread.Start();
            threads.Add(thread);
        }

        foreach (var t in threads) t.Join();

        return picture;
    }
}