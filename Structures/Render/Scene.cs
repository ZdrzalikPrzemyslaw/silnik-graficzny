using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render;

public class Scene : Figure
{
    private readonly List<ComplexFigure> _figures = new();

    public Scene()
    {
    }

    public Scene(List<SimpleFigure?> simpleFigures) : this(simpleFigures.Where(i => i is not null).Cast<SimpleFigure>()
        .ToArray())
    {
    }

    public Scene(params SimpleFigure[] figures)
    {
        _figures.Add(new ComplexFigure(figures));
    }
    
    public Scene(params ComplexFigure[] figures)
    {
        _figures.AddRange(figures);
    }
    
    public Scene(ComplexFigure figure)
    {
        _figures.Add(figure);
    }


    public override bool Equals(Figure? other)
    {
        return Equals(other as Scene);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Scene);
    }

    public override int GetHashCode()
    {
        return _figures.GetSequenceHashCode();
    }

    protected bool Equals(Scene? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _figures.SequenceEqual(other._figures);
    }

    public override bool Intersects(Ray ray)
    {
        return _figures.Any(figure => figure.Intersects(ray));
    }

    public override Vector3? Intersection(Ray ray)
    {
        var intersections = Intersections(ray);
        if (intersections.Count == 0) return null;
        var closest = intersections[0];
        var closestDistance = closest.Distance(ray);
        foreach (var intersection in intersections)
        {
            var loopDistance = intersection.Distance(ray);
            if (closestDistance > loopDistance)
            {
                closestDistance = loopDistance;
                closest = intersection;
            }
        }

        return closest;
    }

    public override List<Vector3> Intersections(Ray ray)
    {
        List<Vector3> returnList = new();
        foreach (var figure in _figures) returnList.AddRange(figure.Intersections(ray));

        return returnList;
    }

    public SimpleFigure? GetClosest(Ray ray)
    {
        Intersection? closest = null;
        this._figures.ForEach(i =>
        {
            var intersection = i.GetClosest(ray);
            if (closest is null)
            {
                closest = intersection;
            }
            else if (intersection is not null)
            {
                closest = ray.Origin.Distance(intersection.Position) < ray.Origin.Distance(closest.Position)
                    ? intersection
                    : closest;
            }
        });
        return closest?.Figure ?? null;
    }

    public void AddFigure(SimpleFigure figure)
    {
        _figures.Add(new ComplexFigure(figure));
    }
    
    public void AddFigure(ComplexFigure figure)
    {
        _figures.Add(figure);
    }

    // To usuwa po identycznosci, nie po referencji
    public void RemoveFigure(ComplexFigure figure)
    {
        _figures.Remove(figure);
    }
}