using Structures.MathObjects;
using Structures.Render;

namespace Structures.Figures;

// TODO: to jest prawie to samo co scena i nwm co z tym fantem zrobic
//  Tylko scena ma ComplexFigure a nie SimpleFigure 
//  W szczegolnosci chodzi mi o metody przeciec
public class ComplexFigure : Figure
{
    
    private readonly List<SimpleFigure> _figures = new();
    
    public string Name { get; private init; }
    
    public ComplexFigure() : this(string.Empty)
    {
    }
    
    public ComplexFigure(string name) : this(Array.Empty<SimpleFigure>(), name) {}

    public ComplexFigure(IEnumerable<SimpleFigure?> simpleFigures) : this(simpleFigures.Where(i => i is not null).Cast<SimpleFigure>()
        .ToArray())
    {
    }

    public ComplexFigure(params SimpleFigure[] figures) : this(figures, string.Empty)
    {
    }
    
    public ComplexFigure(string name, params SimpleFigure[] figures) : this(figures, name)
    {
    }
    
    public ComplexFigure(IEnumerable<SimpleFigure> figures, string name) 
    {
        _figures.AddRange(figures);
        Name = name;
    }
    
    public ComplexFigure(SimpleFigure figure, string name) : this (new []{figure}, name)
    {

    }
    
    public override bool Equals(Figure? other)
    {
        throw new NotImplementedException();
    }
    
    public Intersection? GetClosest(Ray ray)
    {
        Intersection? intersection = null;
        var closestDistance = double.MaxValue;
        foreach (var figure in _figures)
            try
            {
                var x = figure.Intersections(ray);
                foreach (var position in x)
                {
                    var distance = position.Distance(ray);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        intersection = new Intersection(figure, position);
                    }
                }
            }
            catch (Plane.InfiniteIntersectionsException)
            {
            }

        return intersection;
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

    
    public void AddFigure(SimpleFigure figure)
    {
        _figures.Add(figure);
    }
    
    // To usuwa po identycznosci, nie po referencji
    public void RemoveFigure(SimpleFigure figure)
    {
        _figures.Remove(figure);
    }
}