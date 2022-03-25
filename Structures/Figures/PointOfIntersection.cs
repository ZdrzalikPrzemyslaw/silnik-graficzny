using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Figures;

public record PointOfIntersection
{
    private readonly LightIntensity _intensity;

    public PointOfIntersection(SimpleFigure? figure, Vector3 position, LightIntensity? lightIntensity = null)
    {
        Figure = figure;
        Position = position;
        Intensity = lightIntensity;
    }

    public LightIntensity? Intensity
    {
        init => _intensity = value ?? new LightIntensity(0, 0, 0);
    }


    public SimpleFigure? Figure { get; }

    public Vector3 Position { get; }
}