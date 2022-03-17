using Structures.Render;

namespace Structures.Figures;

public abstract class SimpleFigure : Figure
{
    public LightIntensity LightIntensity { get; protected set; } = LightIntensity.DefaultObject();
}