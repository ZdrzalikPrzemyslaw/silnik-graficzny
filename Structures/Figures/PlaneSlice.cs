﻿using Structures.MathObjects;
using Structures.Surface;

namespace Structures.Figures;

public class PlaneSlice : Plane
{
    public PlaneSlice(Vector3 inNormal, Vector3 point, Vector3 leftUpPoint,
        Vector3 rightUpPoint, Vector3 rightDownPoint, Vector3 leftDownPoint, Material? material = null) : base(inNormal, point, material)
    {
        LeftUpPoint = leftUpPoint;
        RightUpPoint = rightUpPoint;
        RightDownPoint = rightDownPoint;
        LeftDownPoint = leftDownPoint;
    }

    public PlaneSlice(Vector3 inNormal, double distance, Material? material = null) : base(inNormal, distance, material)
    {
    }

    public PlaneSlice(Vector3 inNormal, Vector3 point, Material? material = null) : base(inNormal, point, material)
    {
    }

    public Vector3 LeftUpPoint { get; set; } = Vector3.Zero();
    public Vector3 RightUpPoint { get; set; } = Vector3.Zero();
    public Vector3 RightDownPoint { get; set; } = Vector3.Zero();
    public Vector3 LeftDownPoint { get; set; } = Vector3.Zero();

    public override PointOfIntersection? Intersection(Ray ray)
    {
        try
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
        catch (InfiniteIntersectionsException e)
        {
            Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public (double, double) GetPercentageOfPoint(Vector3 point)
    {
        var AE = LeftUpPoint.Distance(point);
        var AC = LeftUpPoint.Distance(LeftDownPoint);
        var AB = LeftUpPoint.Distance(RightUpPoint);
        var AD = LeftUpPoint.Distance(RightDownPoint);
        var AEPrim = AC * AE / AD;
        var AEPrimPrim = AB * AE / AD;
        return (AEPrim, AEPrimPrim);
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
        catch (InfiniteIntersectionsException e)
        {
            Console.WriteLine(e.StackTrace);
            return new List<PointOfIntersection>();
        }
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