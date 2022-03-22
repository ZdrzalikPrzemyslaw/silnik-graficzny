using Structures.Figures;
using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render;

public class Scene : AbstractFigureList<ComplexFigure>
{
    private readonly List<ComplexFigure> _figures = new();
    private readonly List<LightSource> _lightSources = new();

    public Scene()
    {
    }

    public Scene(List<SimpleFigure?> simpleFigures) : this(simpleFigures.Where(i => i is not null).Cast<SimpleFigure>()
        .ToArray())
    {
    }

    public Scene(List<SimpleFigure?> simpleFigures, List<LightSource?> lightSources) : this(simpleFigures
        .Where(i => i is not null).Cast<SimpleFigure>()
        .ToArray(), lightSources
        .Where(i => i is not null).Cast<LightSource>()
        .ToArray())
    {
    }

    public Scene(params SimpleFigure[] figures)
    {
        _figures.Add(new ComplexFigure(figures));
    }

    public Scene(SimpleFigure[] figures, LightSource[] lightSources)
    {
        _figures.Add(new ComplexFigure(figures));
        _lightSources.AddRange(lightSources);
    }

    public Scene(params ComplexFigure[] figures)
    {
        _figures.AddRange(figures);
    }

    public Scene(ComplexFigure[] figures, LightSource[] lightSources)
    {
        _figures.AddRange(figures);
        _lightSources.AddRange(lightSources);
    }

    public Scene(ComplexFigure figure)
    {
        _figures.Add(figure);
    }

    public override PointOfIntersection? GetClosest(Ray ray)
    {
        PointOfIntersection? intersection = null;
        _figures.ForEach(figure =>
        {
            var closest1 = figure.GetClosest(ray);
            if (intersection is null)
                intersection = closest1;
            else if (closest1 is not null)
                intersection = ray.Origin.Distance(closest1.Position) < ray.Origin.Distance(intersection.Position)
                    ? closest1
                    : intersection;
        });
        return intersection;
    }

    public void AddFigure(SimpleFigure figure)
    {
        _figures.Add(new ComplexFigure(figure));
    }

    protected override List<ComplexFigure> GetList()
    {
        return _figures;
    }
}