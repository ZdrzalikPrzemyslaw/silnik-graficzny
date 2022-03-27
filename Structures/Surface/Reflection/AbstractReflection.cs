using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Surface.Reflection;

public abstract class AbstractReflection : IReflection
{
    public abstract Ray GetReflectedRay(Ray lightRay, PointOfIntersection pointOfIntersection);
}