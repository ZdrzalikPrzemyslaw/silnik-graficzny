using System.Collections.ObjectModel;
using Structures.Figures;
using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render;

public class Scene : AbstractFigureList<ComplexFigure>
{
    private readonly List<ComplexFigure> _figures = new();
    private readonly List<LightSourceArray> _lightSources = new();

    public Scene()
    {
    }

    public Scene(List<Figure?> Figures) : this(Figures.Where(i => i is not null).Cast<Figure>()
        .ToArray())
    {
    }

    public Scene(List<Figure?> Figures, List<AbstractLightSource?> lightSources) : this(Figures
        .Where(i => i is not null).Cast<Figure>()
        .ToArray(), lightSources
        .Where(i => i is not null).Cast<AbstractLightSource>()
        .ToArray())
    {
    }

    public Scene(params Figure[] figures)
    {
        _figures.Add(new ComplexFigure(figures));
    }

    public Scene(Figure[] figures, AbstractLightSource[] lightSources)
    {
        _figures.Add(new ComplexFigure(figures));
        _lightSources.Add(new LightSourceArray(lightSources));
    }

    public Scene(params ComplexFigure[] figures)
    {
        _figures.AddRange(figures);
    }

    public Scene(ComplexFigure[] figures, AbstractLightSource[] lightSources)
    {
        _figures.AddRange(figures);
        _lightSources.Add(lightSources);
    }

    public Scene(ComplexFigure figure)
    {
        _figures.Add(figure);
    }

    public override PointOfIntersection? Intersection(Ray ray)
    {
        var pointOfIntersection = base.Intersection(ray);
        if (pointOfIntersection == null) return null;
        var lightIntensity = new LightIntensity();
        foreach (var lightSourceArray in _lightSources)
        {
            foreach (var lightSource in lightSourceArray)
            {
                lightIntensity += !lightSource.IsInShadow(pointOfIntersection, this)
                    ? lightSource.GetIntensity(pointOfIntersection.Position)
                    : new LightIntensity();  
            }
        }

        return new PointOfIntersection(pointOfIntersection.Figure,
            pointOfIntersection.Position, lightIntensity);
    }


    public LightIntensity GetLightIntensity(PointOfIntersection? pointOfIntersection)
    {
        //TODO: Poprawić
        LightIntensity.LightIntensityBuilder lightIntensityBuilder = new();
        if (pointOfIntersection is null) return lightIntensityBuilder.Build();
        foreach (var lightSourceArray in _lightSources)
        {
            foreach (var lightSource in lightSourceArray)
            {
                if (lightSource.IsInShadow(pointOfIntersection, this)) continue;
                lightIntensityBuilder += lightSource.GetIntensity(pointOfIntersection);
            }
        }

        return lightIntensityBuilder.Build();
    }

    public void AddFigure(Figure figure)
    {
        _figures.Add(new ComplexFigure(figure));
    }

    public void AddLight(AbstractLightSource lightSource)
    {
        _lightSources.Add(new AbstractLightSource[] { lightSource});
    }

    protected override List<ComplexFigure> GetList()
    {
        return _figures;
    }

    public ReadOnlyCollection<LightSourceArray> GetReadOnlyLightList()
    {
        return _lightSources.AsReadOnly();
    }

    public ReadOnlyCollection<ComplexFigure> GetReadOnlyFiguresList()
    {
        return _figures.AsReadOnly();
    }
}