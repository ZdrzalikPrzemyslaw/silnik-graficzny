namespace Structures;

public class Scene : IRaycastable
{
    private readonly List<Figure> _figures = new();

    public Scene()
    {
    }

    public Scene(params Figure[] figures)
    {
        _figures.AddRange(figures);
    }


    // todo
    public bool Intersects(Ray ray)
    {
        return _figures.Any(figure => figure.Intersects(ray));
    }

    public Vector3? Intersection(Ray ray)
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

    public List<Vector3> Intersections(Ray ray)
    {
        List<Vector3> returnList = new();
        foreach (var figure in _figures) returnList.AddRange(figure.Intersections(ray));

        return returnList;
    }

    public void AddFigure(Figure figure)
    {
        _figures.Add(figure);
    }

    // To usuwa po identycznosci, nie po referencji
    public void RemoveFigure(Figure figure)
    {
        _figures.Remove(figure);
    }
}