using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public abstract class ComplexLightSource : AbstractLightSource
{
    protected ComplexLightSource(LightIntensity colour) : base(colour)
    {
    }

    public abstract Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);

    public abstract Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);
}