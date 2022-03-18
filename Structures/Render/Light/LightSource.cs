using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public abstract class LightSource : ILightSource
{
    public LightIntensity Colour { get; set; }

    public abstract Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);

    public abstract Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);
    public abstract bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene);
}