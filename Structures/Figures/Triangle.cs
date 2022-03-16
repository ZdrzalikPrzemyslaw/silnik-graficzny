using Structures.MathObjects;
using Structures.Render;

namespace Structures.Figures;

public class Triangle : Figure
{
    public Vector3 A { get; set; } = Vector3.Zero();
    private Plane _plane;
    public Vector3 B { get; set; } = Vector3.Zero();
    public Vector3 C { get; set; } = Vector3.Zero();


    public Triangle(Vector3 inNormal, Vector3 point, LightIntensity lightIntensity, Vector3 a,
        Vector3 b, Vector3 c)
    {
        _plane = new Plane(inNormal, point, lightIntensity);
        A = a;
        B = b;
        C = c;
    }

    public override bool Equals(Figure? other)
    {
        throw new NotImplementedException();
    }

    public override bool Intersects(Ray ray)
    {
        Vector3? planeIntersectionPoint = null;
        try
        {
            planeIntersectionPoint = _plane.Intersection(ray);
        }
        catch (Plane.InfiniteIntersectionsException)
        {
        }
        if (planeIntersectionPoint is null)
        {
            return false;
        }

        throw new NotImplementedException();

    }

    public override Vector3? Intersection(Ray ray)
    {
        throw new NotImplementedException();
    }

    public override List<Vector3> Intersections(Ray ray)
    {
        throw new NotImplementedException();
    }
}