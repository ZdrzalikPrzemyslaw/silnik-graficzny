using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render;

public interface IRaycastable
{
    // Czy sie przecina
    bool Intersects(Ray ray);

    // Najblizsze przeciecie
    PointOfIntersection? Intersection(Ray ray);

    // Wszystkie przeciecia
    List<PointOfIntersection> Intersections(Ray ray);
}