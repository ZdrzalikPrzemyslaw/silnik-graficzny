using Structures.MathObjects;
using Structures.Render.Light;
using Structures.Surface;

namespace Structures.Figures;

public class PlaneSlice : Plane
{
    public PlaneSlice(Vector3 inNormal, Vector3 point, Vector3 leftUpPoint,
        Vector3 rightUpPoint, Vector3 rightDownPoint, Vector3 leftDownPoint, Material? material = null) : base(inNormal,
        point, material)
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
            // Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    //https://www.obliczeniowo.com.pl/172
    private (double, double) GetPercentageOfPoint(Vector3 point)
    {
        var u = point.Dot(RightUpPoint - LeftUpPoint) /
                (LeftUpPoint - RightUpPoint).Dot(LeftUpPoint - RightUpPoint);

        var v = point.Dot(LeftDownPoint - LeftUpPoint) /
                (LeftUpPoint - LeftDownPoint).Dot(LeftUpPoint - LeftDownPoint);
        return (u, v);
    }

    public override LightIntensity GetTexture(Vector3 point)
    {
        if (Material.Texture is null) return LightIntensity.DefaultWhite();
        point -= LeftUpPoint;
        var (u, v) = GetPercentageOfPoint(point);
        return Material.Texture.ColorMap[(int)(v * (Material.Texture.ColorMap.GetLength(0) - 1)),
            (int)(u * (Material.Texture.ColorMap.GetLength(1) - 1))];
    }

    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        try
        {
            var intersection = Intersection(ray);
            return intersection is not null
                ? new List<PointOfIntersection> { intersection }
                : new List<PointOfIntersection>();
        }
        catch (InfiniteIntersectionsException e)
        {
            // Console.WriteLine(e.StackTrace);
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