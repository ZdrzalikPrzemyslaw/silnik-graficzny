using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public interface ILightSource
{
    public LightIntensity GetIntensity(Vector3 position);
    public abstract bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene);
}