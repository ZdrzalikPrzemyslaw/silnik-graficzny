using SkiaSharp;

namespace Structures;

public class OrthogonalCamera : AbstractCamera
{
    private readonly Picture _picture = new(800, 800);

    private readonly Sphere _sphere = new(new Vector3(0, 0, 5), 1);
    private readonly double Height = 4.0;

    private readonly double Width = 4.0;

    public OrthogonalCamera()
    {
    }

    public OrthogonalCamera(Vector3 position, Vector3 target) : base(position, target)
    {
    }

    public override void RenderScene()
    {
        var pixelWidth = Width / _picture.Bitmap.Width;
        var pixelHeight = Height / _picture.Bitmap.Height;
        for (var i = 0; i < _picture.Bitmap.Width; i++)
        for (var j = 0; j < _picture.Bitmap.Height; j++)
        {
            var srodekX = -Height / 2 + (i + 0.5f) * pixelWidth;
            var srodekY = Width / 2 - (j + 0.5f) * pixelHeight;
            var ray = new Ray(new Vector3(srodekX, srodekY, 0), new Vector3(0, 0, 1));
            var intersetion = _sphere.Intersection(ray);
            if (intersetion is not null)
                _picture.SetPixel(i, j, new LightIntensity(1, 0.78, 0.64));
            else _picture.SetPixel(i, j, new LightIntensity(0.64, 0.67, 1));
        }

        using (var data = _picture.Bitmap.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(Path.Combine("./", "Picture.png")))
        {
            data.SaveTo(stream);
        }
    }
}