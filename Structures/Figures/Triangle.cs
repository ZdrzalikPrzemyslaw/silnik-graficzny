using Structures.MathObjects;

namespace Structures.Figures;

public class Triangle : Figure
{
    private readonly Plane _plane;

    public Triangle(Vector3 a, Vector3 b, Vector3 c)
    {
        _plane = new Plane(a, b, c);
        A = a;
        B = b;
        C = c;
    }

    public Vector3 A { get; set; }
    public Vector3 B { get; set; }
    public Vector3 C { get; set; }

    public override Vector3 GetNormal(PointOfIntersection? pointOfIntersection = null)
    {
        return _plane.Normal;
    }

    public override bool Equals(Figure? other)
    {
        return Equals(other as Triangle);
    }

    public override bool Intersects(Ray ray)
    {
        try
        {
            return Intersection(ray) is not null;
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    protected bool Equals(Triangle? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _plane.Equals(other._plane) && A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Triangle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_plane, A, B, C);
    }

    public override PointOfIntersection? Intersection(Ray ray)
    {
        PointOfIntersection? planeIntersectionPoint = null;
        try
        {
            planeIntersectionPoint = _plane.Intersection(ray);
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            Console.WriteLine(e.StackTrace);
            return null;
        }

        if (planeIntersectionPoint is null) return null;

        var vA = A - planeIntersectionPoint.Position;
        var vB = B - planeIntersectionPoint.Position;
        var vC = C - planeIntersectionPoint.Position;
        var vX = vA.Cross(vB);

        if (vX.Dot(_plane.Normal) < 0) return null;
        vX = vB.Cross(vC);
        if (vX.Dot(_plane.Normal) < 0) return null;
        vX = vC.Cross(vA);
        if (vX.Dot(_plane.Normal) < 0) return null;

        return planeIntersectionPoint;
    }

    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        try
        {
            var intersection = Intersection(ray);
            return intersection is not null
                ? new List<PointOfIntersection> {intersection}
                : new List<PointOfIntersection>();
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            Console.WriteLine(e.StackTrace);
            return new List<PointOfIntersection>();
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Triangle (A: {A}, B: {B}, C: {C}, _plane: {_plane})";
    }
}