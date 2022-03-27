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

    private Ray CalculateDirection(Ray lightRay, PointOfIntersection pointOfIntersection)
    {
        var normal = -pointOfIntersection.Figure.GetNormal(pointOfIntersection);
        var direction = lightRay.Direction;
        var dot = direction.Dot(normal);
        var first = RefractiveIndex * (direction - normal * dot) / RefractiveIndexForAir;
        var second = Math.Pow(RefractiveIndex, 2) * (1 - Math.Pow(dot, 2)) / Math.Pow(RefractiveIndexForAir, 2);
        return new Ray(pointOfIntersection.Position, first - normal * Math.Sqrt(1 - second));
    }

    public override Ray GetReflectedRay(Ray lightRay, PointOfIntersection pointOfIntersection)
    {
        var normal = pointOfIntersection.Figure.GetNormal(pointOfIntersection);
        var direction = lightRay.Direction;
        var dot = direction.Dot(normal);
        var first = RefractiveIndexForAir * (direction - normal * dot) / RefractiveIndex;
        var second = Math.Pow(RefractiveIndexForAir, 2) * (1 - Math.Pow(dot, 2)) / Math.Pow(RefractiveIndex, 2);
        var middleRay = new Ray(pointOfIntersection.Position, first - normal * Math.Sqrt(1 - second));

        var intersections = pointOfIntersection.Figure.Intersections(middleRay);
        if (intersections.Count > 1)
        {
            foreach (var intersection in intersections)
                if (intersection.Position.Distance(pointOfIntersection.Position) > 0.001)
                    return CalculateDirection(middleRay, intersection);
        }
        else if (intersections.Count == 1)
        {
            return CalculateDirection(middleRay, intersections[0]);
        }

        return middleRay;
    }
}