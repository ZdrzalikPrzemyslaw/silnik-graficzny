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
}