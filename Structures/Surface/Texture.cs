using Structures.MathObjects;
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
        return ColorMap[(int) (z * (ColorMap.GetLength(0) - 1)),
            (int) (x * (ColorMap.GetLength(1) - 1))];
    }

    public LightIntensity GetBySphericalMapping(Vector3 point)
    {
        var theta = Math.Acos(point.Y);
        theta = theta is Double.NaN ? 1 : theta; 
        var phi = Math.Atan2(point.X, point.Z);
        phi = phi < 0 ? phi + 2 * Math.PI : phi;
        var u = phi / (2 * Math.PI);
        var v = 1 - theta / Math.PI;
        return ColorMap[(int) (u * (ColorMap.GetLength(0) - 1)),
            (int) (v * (ColorMap.GetLength(1) - 1))];
    }
}