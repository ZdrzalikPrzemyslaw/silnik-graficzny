using Structures.MathObjects;
using Structures.Render.Sampler;

namespace Structures.Render.Camera;

public class OrthogonalCamera : AbstractCamera
{
    public OrthogonalCamera()
    {
        _height = 4.0;
        _width = 4.0;
    }

    public OrthogonalCamera(double height, double width)
    {
        _height = height;
        _width = width;
    }

    public OrthogonalCamera(Vector3 position, Vector3 target, Vector3 up) : base(position, target, up)
    {
        _height = 6.0;
        _width = 6.0;
    }

    public OrthogonalCamera(Vector3 position, Vector3 target, Vector3 up, double height, double width) : base(position,
        target, up)
    {
        _height = height;
        _width = width;
    }

    // TODO: kontruktor czy cos?
    public ISampler Sampler { get; } = new OrthogonalSampler();

    public double _height { get; }

    public double _width { get; }

    private void RenderPiece(Picture picture, Scene scene, int fromX, int toX, int fromY, int toY)
    {
        var pixelWidth = _width / picture.Bitmap.Width;
        var pixelHeight = _height / picture.Bitmap.Height;
        var startX = -_width / 2 + Position.X;
        var startY = _height / 2 + Position.Y;
        for (var i = fromX; i < toX; i++)
        for (var j = fromY; j < toY; j++)
        {
            var locX = startX + i * pixelWidth;
            var locY = startY - j * pixelHeight;
            var ray = new Ray(new Vector3(locX, locY, Position.Z), Target);
            var intersection = Sampler.Sample(scene, ray, pixelWidth, Up);

            picture.SetPixel(i, j, intersection);
        }
  
    }

    public override Picture RenderScene(Scene scene)
    {
        int size = 500;
        Picture picture = new(size, size);
        
        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
        {
            {
                int copyI = i;
                int copyJ = j;
                var thread = new Thread(() => RenderPiece(
                    picture,
                    scene,
                    (size / 4) * copyI,
                    (size / 4) * (copyI + 1),
                    (size / 4) * copyJ,
                    (size / 4) * (copyJ + 1)
                ));
                thread.Start();
                threads.Add(thread);
            }
        }
        foreach (var t in threads)
        {
            t.Join();
        }

        
        return picture;
    }
}