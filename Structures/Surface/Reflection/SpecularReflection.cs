using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Surface.Reflection;

public class SpecularReflection : AbstractReflection
{
    public override Ray GetReflectedRay(Ray lightRay, PointOfIntersection pointOfIntersection)
    {
        var reflectedRayDirection =
            lightRay.Direction - 2 * pointOfIntersection.Figure.GetNormal(pointOfIntersection) * (pointOfIntersection.Figure.GetNormal(pointOfIntersection) * lightRay.Direction);
        return new Ray(pointOfIntersection.Position, reflectedRayDirection);
    }
}