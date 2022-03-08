using SkiaSharp;

namespace Structures;

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
}