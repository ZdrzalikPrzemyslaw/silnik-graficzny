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

    public void AddFigure(Figure figure)
    {
        _figures.Add(figure);
    }

    public bool Intersects(Ray ray)
    {
        throw new NotImplementedException();
    }

    public Vector3? Intersection(Ray ray)
    {
        throw new NotImplementedException();
    }

    public List<Vector3> Intersections(Ray ray)
    {
        throw new NotImplementedException();
    }
}