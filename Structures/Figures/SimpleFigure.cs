using Structures.Render;
using Structures.Render.Light;

namespace Structures.Figures;

public abstract class SimpleFigure : Figure
{
    public LightIntensity LightIntensity { get; protected set; } = LightIntensity.DefaultObject();
}