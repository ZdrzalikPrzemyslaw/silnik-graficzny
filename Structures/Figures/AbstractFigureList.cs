using Structures.MathObjects;

namespace Structures.Figures;

public abstract class AbstractFigureList<T> : Figure where T : Figure
{
    protected abstract List<T> GetList();

    public void AddFigure(T figure)
    {
        GetList().Add(figure);
    }

    public void AddFigure(List<T> figures)
    {
        GetList().AddRange(figures);
    }

    public void RemoveFigure(T figure)
    {
        GetList().Remove(figure);
    }

    public bool Equals(AbstractFigureList<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || GetList().SequenceEqual(other.GetList());
    }

    public override bool Equals(object? other)
    {
        return Equals(other as AbstractFigureList<T>);
    }

    public override bool Equals(Figure? other)
    {
        return Equals(other as AbstractFigureList<T>);
    }

    public override int GetHashCode()
    {
        return GetList().GetSequenceHashCode();
    }

    public override string ToString()
    {
        return GetList().DeepToString();
    }

    public override bool Intersects(Ray ray)
    {
        return GetList().Any(figure => figure.Intersects(ray));
    }

    public override PointOfIntersection? Intersection(Ray ray)
    {
        var intersections = Intersections(ray);
        if (intersections.Count == 0) return null;
        var closest = intersections[0];
        var closestDistance = closest.Position.Distance(ray);
        foreach (var intersection in intersections)
        {
            var loopDistance = intersection.Position.Distance(ray);
            if (closestDistance > loopDistance)
            {
                closestDistance = loopDistance;
                closest = intersection;
            }
        }

        return closest;
    }

    public override List<PointOfIntersection> Intersections(Ray ray)
    {
        List<PointOfIntersection> returnList = new();
        foreach (var figure in GetList()) returnList.AddRange(figure.Intersections(ray));

        return returnList;
    }
}