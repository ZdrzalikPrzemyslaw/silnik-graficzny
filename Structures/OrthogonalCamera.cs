﻿using SkiaSharp;

namespace Structures;

public class OrthogonalCamera : AbstractCamera
{
    public override void RenderScene()
    {
        var pixelWidth = 2.0d / _picture.Bitmap.Width;
        var pixelHeight = 2.0d / _picture.Bitmap.Height;

        for (var i = 0; i < _picture.Bitmap.Width; i++)
        for (var j = 0; j < _picture.Bitmap.Height; j++)
        {
            var srodekX = -1.0f + (i + 0.5f) * pixelWidth;
            var srodekY = 1.0f - (j + 0.5f) * pixelHeight;
            var ray = new Ray(new Vector3(0, 0, 1), new Vector3(srodekX, srodekY, 0));
            var intersetion = _sphere.NearestIntersection(ray);
            if (intersetion is not null)
                _picture.SetPixel(i, j, new LightIntensity(1, 1, 1));
            else _picture.SetPixel(i, j, new LightIntensity(0, 0, 0));
        }

        // _picture.Bitmap.Encode() .Save("obrazkoncowy.jpg");
        using (var data = _picture.Bitmap.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(Path.Combine("D:", "Picture.png")))
        {
            data.SaveTo(stream);
        }
    }

    private double Width = 2.0;
    private double Height = 2.0;

    private readonly Picture _picture = new(400, 300);

    private readonly Sphere _sphere = new(new Vector3(5, 0, 0), 3);

    public OrthogonalCamera()
    {
    }

    public OrthogonalCamera(Vector3 position, Vector3 target) : base(position, target)
    {
    }
}