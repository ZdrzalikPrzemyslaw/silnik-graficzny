using Structures.Render.Light;

namespace Structures.Surface;

public class Texture
{
    public int Width { get; set; }
    public int Height { get; set; }
    public LightIntensity[,] ColorMap { get; set; } = { };

    public Texture(int width, int height, LightIntensity[,] colorMap)
    {
        Width = width;
        Height = height;
        ColorMap = colorMap;
    }

    public LightIntensity RectangularMapping(double x, double z)
    {
        return ColorMap[(int) ((z + 1) / Width), (int) ((x - 1) / Height)];
    }

    public LightIntensity SphericalMapping(double x, double z)
    {
        throw new NotImplementedException();
    }
}