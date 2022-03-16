using Structures.MathObjects;
using Structures.Render;

namespace Structures.Figures;

public class Triangle : Figure
{
    public Vector3 A { get; set; }
    private readonly Plane _plane;
    public Vector3 B { get; set; }
    public Vector3 C { get; set; }


    public Triangle(Plane plane) : this(plane, Vector3.Zero(), Vector3.Zero(), Vector3.Zero())
    {
    }
    
    public Triangle(Plane plane, LightIntensity lightIntensity) : this(plane, lightIntensity, Vector3.Zero(), Vector3.Zero(), Vector3.Zero())
    {
    }
    
    public Triangle(Plane plane, Vector3 a, Vector3 b, Vector3 c) : this(plane.Normal, plane.Distance, plane.LightIntensity, a,b,c)
    {
    }
    
    public Triangle(Plane plane, LightIntensity lightIntensity, Vector3 a, Vector3 b, Vector3 c) : this(plane.Normal, plane.Distance, lightIntensity, a,b,c)
    {
    }
    
    public Triangle(Vector3 inNormal, Vector3 point, LightIntensity lightIntensity, Vector3 a,
        Vector3 b, Vector3 c)
    {
        _plane = new Plane(inNormal, point, lightIntensity);
        A = a;
        B = b;
        C = c;
    }
    
    public Triangle(Vector3 inNormal, double distance, LightIntensity lightIntensity, Vector3 a,
        Vector3 b, Vector3 c)
    {
        _plane = new Plane(inNormal, distance, lightIntensity);
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
        return Intersection(ray) is not null;
    }

    public override Vector3? Intersection(Ray ray)
    {
        Vector3? planeIntersectionPoint = null;
        try
        {
            planeIntersectionPoint = _plane.Intersection(ray);
        }
        catch (Plane.InfiniteIntersectionsException)
        {
        }

        if (planeIntersectionPoint is null) return null;

        var vA = A - planeIntersectionPoint;
        var vB = B - planeIntersectionPoint;
        var vC = C - planeIntersectionPoint;
        var vX = vA.Cross(vB);

        if (vX.Dot(_plane.Normal) < 0) return null;
        vX = vB.Cross(vC);
        if (vX.Dot(_plane.Normal) < 0) return null;
        vX = vC.Cross(vA);
        if (vX.Dot(_plane.Normal) < 0) return null;

        return planeIntersectionPoint;
    }

    public override List<Vector3> Intersections(Ray ray)
    {
        var intersection = Intersection(ray);
        return intersection is not null ? new List<Vector3> {intersection} : new List<Vector3>();
    }
}