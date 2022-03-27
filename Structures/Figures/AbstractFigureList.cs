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
    
    public override Vector3 GetNormal(PointOfIntersection? pointOfIntersection = null)
    {
        //TODO: poprawic wyjatki
        return pointOfIntersection?.Figure?.GetNormal() ?? throw new ArgumentException();
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
        PointOfIntersection? closest = null;
        double closestDistance = double.MaxValue;
        foreach (var intersection in intersections)
        {
            var loopDistance = intersection.Position.Distance(ray);
            if(loopDistance == 0) continue;
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