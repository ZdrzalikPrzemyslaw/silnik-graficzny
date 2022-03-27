using Structures.MathObjects;
using Structures.Render;
using Structures.Render.Light;
using Structures.Surface;

namespace Structures.Figures;

public abstract class Figure : IRaycastable, IEquatable<Figure>
{
    public Material Material { get; set; } = new();
    public abstract bool Equals(Figure? other);
    public abstract bool Intersects(Ray ray);

    public abstract PointOfIntersection? Intersection(Ray ray);

    public abstract List<PointOfIntersection> Intersections(Ray ray);

    public abstract Vector3 GetNormal(PointOfIntersection? pointOfIntersection = null);

    public override bool Equals(object? obj)
    {
        return Equals(obj as Figure);
    }

    public virtual LightIntensity GetTexture(Vector3 point)
    {
        return LightIntensity.DefaultWhite();
    }
}