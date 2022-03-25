using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public interface ILightSource
{
    public LightIntensity GetIntensity(Vector3 position);
    public LightIntensity GetIntensity(PointOfIntersection point);
    public bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene);
}