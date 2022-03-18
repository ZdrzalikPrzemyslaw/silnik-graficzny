using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public interface ILightSource
{
    public Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);
    public Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection);

    public bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene);
}