namespace Structures;

public abstract class Figure : IRaycastable, IEquatable<Figure>
{
    public abstract bool Equals(Figure? other);
    public abstract bool Intersects(Ray ray);

    public abstract Vector3? Intersection(Ray ray);

    public abstract List<Vector3> Intersections(Ray ray);

    public override bool Equals(object obj)
    {
        return Equals(obj as Figure);
    }
}