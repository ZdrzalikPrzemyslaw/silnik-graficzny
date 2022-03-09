namespace Structures;

public abstract class Figure : IRaycastable
{
    public abstract bool Intersects(Ray ray);

    public abstract Vector3? Intersection(Ray ray);

    public abstract List<Vector3> Intersections(Ray ray);
}