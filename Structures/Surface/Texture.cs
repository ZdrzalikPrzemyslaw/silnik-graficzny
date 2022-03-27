using Structures.Render.Light;

namespace Structures.Surface;

public class Texture
{
    public Texture(LightIntensity[,] colorMap)
    {
        ColorMap = colorMap;
    }

    public LightIntensity[,] ColorMap { get; set; } = { };
}