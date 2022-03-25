using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class AmbientLightSource : AbstractLightSource
{
    public AmbientLightSource(LightIntensity lightIntensity) : base(lightIntensity)
    {
    }

    public override LightIntensity GetIntensity(Vector3 position)
    {
        return Colour;
    }

    public override LightIntensity GetIntensity(PointOfIntersection point)
    {
        return Colour;
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        return false;
    }
}