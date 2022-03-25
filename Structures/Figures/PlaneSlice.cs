using Structures.MathObjects;

namespace Structures.Figures;

public class PlaneSlice : Plane
{
    public PlaneSlice(Vector3 inNormal, Vector3 point, Vector3 leftUpPoint,
        Vector3 rightUpPoint, Vector3 rightDownPoint, Vector3 leftDownPoint) : base(inNormal, point)
    {
        LeftUpPoint = leftUpPoint;
        RightUpPoint = rightUpPoint;
        RightDownPoint = rightDownPoint;
        LeftDownPoint = leftDownPoint;
    }

    public PlaneSlice(Vector3 inNormal, double distance) : base(inNormal, distance)
    {
    }

    public PlaneSlice(Vector3 inNormal, Vector3 point) : base(inNormal, point)
    {
    }

    public Vector3 LeftUpPoint { get; set; } = Vector3.Zero();
    public Vector3 RightUpPoint { get; set; } = Vector3.Zero();
    public Vector3 RightDownPoint { get; set; } = Vector3.Zero();
    public Vector3 LeftDownPoint { get; set; } = Vector3.Zero();

    public override PointOfIntersection? Intersection(Ray ray)
    {
        var intersectionPoint = base.Intersection(ray);
        if (intersectionPoint is null) return null;
        var rightUpCorner = (LeftUpPoint.Dot(RightUpPoint - LeftUpPoint) <=
                             intersectionPoint.Position.Dot(RightUpPoint - LeftUpPoint))
                            & (intersectionPoint.Position.Dot(RightUpPoint - LeftUpPoint) <=
                               RightUpPoint.Dot(RightUpPoint - LeftUpPoint));
        var leftDownCorner = (LeftUpPoint.Dot(LeftDownPoint - LeftUpPoint) <=
                              intersectionPoint.Position.Dot(LeftDownPoint - LeftUpPoint))
                             & (intersectionPoint.Position.Dot(LeftDownPoint - LeftUpPoint) <=
                                LeftDownPoint.Dot(LeftDownPoint - LeftUpPoint));
        return rightUpCorner && leftDownCorner ? intersectionPoint : null;
    }

    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        var intersection = Intersection(ray);
        return intersection is not null
            ? new List<PointOfIntersection> { intersection }
            : new List<PointOfIntersection>();
    }

    protected bool Equals(PlaneSlice? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && LeftUpPoint.Equals(other.LeftUpPoint) && RightUpPoint.Equals(other.RightUpPoint) &&
               RightDownPoint.Equals(other.RightDownPoint) && LeftDownPoint.Equals(other.LeftDownPoint);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PlaneSlice);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), LeftUpPoint, RightUpPoint, RightDownPoint, LeftDownPoint);
    }
}