using SkiaSharp;

namespace Structures.Render;

public class Picture
{
    public Picture(int xSize, int ySize)
    {
        Bitmap = new SKBitmap(xSize, ySize);
    }

    public SKBitmap Bitmap { get; set; }

    public void SetPixel(int x, int y, LightIntensity pixel)
    {
        Bitmap.SetPixel(x, y, new SKColor(
            Convert.ToByte(pixel.R * 255),
            Convert.ToByte(pixel.G * 255),
            Convert.ToByte(pixel.B * 255)));
    }

    public void PrintToPath(string path = "./")
    {
        using (var data = Bitmap.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(Path.Combine(path, "Picture.png")))
        {
            data.SaveTo(stream);
        }
    }
}