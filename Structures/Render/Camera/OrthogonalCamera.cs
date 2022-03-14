using Structures.Figures;
using Structures.MathObjects;

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
        _height = 4.0;
        _width = 4.0;
    }

    public OrthogonalCamera(Vector3 position, Vector3 target, Vector3 up, double height, double width) : base(position,
        target, up)
    {
        _height = height;
        _width = width;
    }

    public double _height { get; }

    public double _width { get; }

    public override Picture RenderScene(Scene scene)
    {
        Picture picture = new(800, 800);
        var pixelWidth = _width / picture.Bitmap.Width;
        var pixelHeight = _height / picture.Bitmap.Height;
        var startX = -_width / 2 + Position.X;
        var startY = _height / 2 + Position.Y;
        for (var i = 0; i < picture.Bitmap.Width; i++)
        for (var j = 0; j < picture.Bitmap.Height; j++)
        {
            var locX = startX + i * pixelWidth;
            var locY = startY - j * pixelHeight;
            var ray = new Ray(new Vector3(locX, locY, Position.Z), Target);
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

        return picture;
    }
}