using System.Collections.ObjectModel;
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

    public override PointOfIntersection? Intersection(Ray ray)
    {
        PointOfIntersection? pointOfIntersection = base.Intersection(ray);
        if (pointOfIntersection == null) return null;
        LightIntensity lightIntensity = new LightIntensity();
        foreach (var lightSource in _lightSources)
        {
           lightIntensity += !lightSource.IsInShadow(pointOfIntersection, this) ? lightSource.GetIntensity(pointOfIntersection.Position) : new LightIntensity();
        }

        return new PointOfIntersection(pointOfIntersection.Figure,
            pointOfIntersection.Position, lightIntensity);
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
    
    

    public LightIntensity GetLightIntensity(PointOfIntersection? pointOfIntersection)
    {
        //TODO: Poprawić
        LightIntensity.LightIntensityBuilder lightIntensityBuilder = new();
        if (pointOfIntersection is null)
        {
            return lightIntensityBuilder.Build();
        }
        foreach (var lightSource in _lightSources)
        {
            if(lightSource.IsInShadow(pointOfIntersection, this)) continue;
            lightIntensityBuilder += lightSource.GetIntensity(pointOfIntersection);
        }
        return lightIntensityBuilder.Build();
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

    public void AddFigure(SimpleFigure figure)
    {
        _figures.Add(new ComplexFigure(figure));
    }
    
    public void AddLight(LightSource lightSource)
    {
        _lightSources.Add(lightSource);
    }

    protected override List<ComplexFigure> GetList()
    {
        return _figures;
    }
    
    public ReadOnlyCollection<LightSource> GetReadOnlyLightList()
    {
        return _lightSources.AsReadOnly();
    }
    
    public ReadOnlyCollection<ComplexFigure> GetReadOnlyFiguresList()
    {
        return _figures.AsReadOnly();
    }
}