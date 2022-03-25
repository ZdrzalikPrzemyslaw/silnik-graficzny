using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public abstract class LightSource : ILightSource
{
    protected LightSource(LightIntensity colour)
    {
        Colour = colour ?? throw new ArgumentNullException(nameof(colour));
    }

    protected internal LightIntensity Colour { get; }

    public abstract LightIntensity GetIntensity(Vector3 position);
    public abstract LightIntensity GetIntensity(PointOfIntersection point);

    public abstract bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene);
}