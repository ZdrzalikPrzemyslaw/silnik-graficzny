using Structures.MathObjects;

namespace Structures.Render;

public interface IRaycastable
{
    // Czy sie przecina
    bool Intersects(Ray ray);

    // Najblizsze przeciecie
    Vector3? Intersection(Ray ray);

    // Wszystkie przeciecia
    List<Vector3> Intersections(Ray ray);
}