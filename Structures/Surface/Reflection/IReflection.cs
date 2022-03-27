using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Surface.Reflection;

public interface IReflection
{
    public Ray GetReflectedRay(Ray lightRay, PointOfIntersection pointOfIntersection);
}