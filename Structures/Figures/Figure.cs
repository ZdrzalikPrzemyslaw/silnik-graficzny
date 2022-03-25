using Structures.MathObjects;
using Structures.Render;

namespace Structures.Figures;

public abstract class Figure : IRaycastable, IEquatable<Figure>
{
    // Todo: doddac referenje do materiału
    public abstract bool Equals(Figure? other);
    public abstract bool Intersects(Ray ray);

    public abstract PointOfIntersection? Intersection(Ray ray);

    public abstract List<PointOfIntersection> Intersections(Ray ray);

    public override bool Equals(object? obj)
    {
        return Equals(obj as Figure);
    }
}