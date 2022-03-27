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

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up, Plane nearPlane, Plane farPlane, double fovX,
        double fovY)
        : base(position, target, up)
    {
        NearPlane = nearPlane;
        FarPlane = farPlane;
        FovX = fovX;
        FovY = fovY;
    }

    public PerspectiveCamera(Vector3 position, Vector3 target, Vector3 up)
        : this(position, target, up,
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1)),
            new Plane(target, new Ray(position, target).PointAtDistanceFromOrigin(1000)),
            80, 80)
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
        Matrix matrixX = null;
        Matrix matrixY = null;
        LightIntensity intersection = null;
        for (var i = fromX; i < toX; i++)
        {
            matrixX = Matrix.Rotate((startX + i * pixelWidth) * Math.PI / 180, Up);
            for (var j = fromY; j < toY; j++)
            {
                matrixY = Matrix.Rotate((startY + -j * pixelHeight) * Math.PI / 180, Target.Cross(Up));
                ray = new Ray(Position, Target).Rotate(matrixY * matrixX);

                intersection = Sampler.Sample(scene, ray, pixelWidth, pixelHeight, Up);

                picture.SetPixel(i, j, intersection);
            }
        }
    }

    public override Picture RenderScene(Scene scene, int sizeX, int sizeY)
    {
        Picture picture = new(sizeX, sizeY);
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