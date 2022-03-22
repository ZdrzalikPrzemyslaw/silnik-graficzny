using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public abstract class ComplexLightSource : LightSource
{
    public abstract Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);

    public abstract Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);

    protected ComplexLightSource(LightIntensity colour) : base(colour)
    {
    }
}