using Structures.Render.Light;

namespace Structures.Surface;

public class Texture
{
    public LightIntensity[,] ColorMap { get; set; } = { };

    public Texture(LightIntensity[,] colorMap)
    {
        ColorMap = colorMap;
    }

    public LightIntensity GetByRectangularMapping(double x, double z)
    {
        return ColorMap[(int) (z * ColorMap.GetLength(0)),
            (int) (x * ColorMap.GetLength(1))];
    }

    public LightIntensity GetBySphericalMapping(double x, double z)
    {
        throw new NotImplementedException();
    }
}