using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Surface.Reflection;

public class RefractiveReflection : AbstractReflection
{
    private readonly double RefractiveIndex;
    private readonly double RefractiveIndexForAir = 1.00029;

    public RefractiveReflection(double refractiveIndex)
    {
        RefractiveIndex = refractiveIndex;
    }

    public override Ray GetReflectedRay(Ray lightRay, PointOfIntersection pointOfIntersection)
    {
        var normal = pointOfIntersection.Figure.GetNormal(pointOfIntersection);
        var direction = lightRay.Direction;
        var dot = direction.Dot(normal);
        var first = RefractiveIndexForAir * (direction - normal * dot) / RefractiveIndex;
        var second = Math.Pow(RefractiveIndexForAir, 2) * (1 - Math.Pow(dot, 2)) / Math.Pow(RefractiveIndex, 2);
        // todo: tutaj oszustwo takie, że wyznaczmy ten promien ktory wyznaczamy,
        // nastepnie jesgo przeciecie z samym sobą i zwracamy promien ktory zaczyna sie w drugim punkcie przeciecia, z oryginalnym zwrotem
        return new Ray(pointOfIntersection.Position, first - normal * Math.Sqrt(1 - second));
    }
}