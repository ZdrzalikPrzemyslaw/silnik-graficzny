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

    public Scene(List<Figure?> figures) : this(figures.Where(i => i is not null).Cast<Figure>()
        .ToArray())
    {
    }

    public Scene(List<Figure?> figures, List<AbstractLightSource?> lightSources) : this(figures
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
        foreach (var lightSource in lightSourceArray)
            lightIntensity += !lightSource.IsInShadow(pointOfIntersection, this)
                ? lightSource.GetIntensity(pointOfIntersection.Position)
                : new LightIntensity();

        return new PointOfIntersection(pointOfIntersection.Figure,
            pointOfIntersection.Position, lightIntensity);
    }


    public LightIntensity GetLightIntensity(Ray ray)
    {
        var pointOfIntersection = Intersection(ray);
        LightIntensity.LightIntensityBuilder lightIntensityBuilder = new();
        if (pointOfIntersection is null) return lightIntensityBuilder.Build();
        foreach (var lightSourceArray in _lightSources)
        foreach (var lightSource in lightSourceArray)
        {
            if (lightSource.IsInShadow(pointOfIntersection, this)) continue;
            if (lightSource is PointLightSource)
            {
                var I = ray.Direction;
                if (pointOfIntersection.Figure is null) continue;
                var N = pointOfIntersection.Figure.GetNormal(pointOfIntersection);
                var R = I - N * N.Dot(I) * 2.0;
                var ss = ray.Direction.Dot(R);
                var specular = 0.0;
                if (ss < 0) specular = Math.Pow(ss, pointOfIntersection.Figure.Material.ShinessConstant);

                specular *= pointOfIntersection.Figure.Material.KSpecular;
                var sIntensity = lightSource.Colour * specular;
                var cos = ray.Direction.Dot(N);
                var r = -lightSource.Colour.R * pointOfIntersection.Figure.Material.KDiffuse * cos;
                var g = -lightSource.Colour.G * pointOfIntersection.Figure.Material.KDiffuse * cos;
                var b = -lightSource.Colour.B * pointOfIntersection.Figure.Material.KDiffuse * cos;
                lightIntensityBuilder += new LightIntensity(r, g, b);
                lightIntensityBuilder += sIntensity;
            }
            else
            {
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
        _lightSources.Add(new[] {lightSource});
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