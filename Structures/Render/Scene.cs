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

    public override PointOfIntersection? Intersection(Ray ray, Figure? ignore = null)
    {
        try
        {
            var pointOfIntersection = base.Intersection(ray, ignore);
            if (pointOfIntersection == null) return null;

            return new PointOfIntersection(pointOfIntersection.Figure,
                pointOfIntersection.Position);
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            // Console.WriteLine(e.StackTrace);
            return null;
        }
    }
    
    public override PointOfIntersection? Intersection(Ray ray)
    {
        try
        {
            var pointOfIntersection = base.Intersection(ray);
            if (pointOfIntersection == null) return null;

            return new PointOfIntersection(pointOfIntersection.Figure,
                pointOfIntersection.Position);
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            // Console.WriteLine(e.StackTrace);
            return null;
        }
    }


    public LightIntensity GetLightIntensity(Ray ray)
    {
        LightIntensity.LightIntensityBuilder lightIntensityBuilder = new();
        try
        {
            var pointOfIntersection = Intersection(ray);
            if (pointOfIntersection is null) return lightIntensityBuilder.Build();
            if (pointOfIntersection.Figure is null) return lightIntensityBuilder.Build();

            foreach (var lightSourceArray in _lightSources)
            foreach (var lightSource in lightSourceArray)
                lightIntensityBuilder += GetLightIntensityForLightSource(ray, pointOfIntersection, lightSource);
            return lightIntensityBuilder.Build();
        }
        catch (Plane.InfiniteIntersectionsException e)
        {
            // Console.WriteLine(e.StackTrace);
            return lightIntensityBuilder.Build();
        }
    }

    private LightIntensity.LightIntensityBuilder GetLightIntensityForLightSource(Ray ray,
        PointOfIntersection pointOfIntersection, AbstractLightSource lightSource, int recursionCount = 3)
    {
        if (pointOfIntersection.Figure is null)
            return new LightIntensity.LightIntensityBuilder();
        LightIntensity.LightIntensityBuilder lightIntensityBuilder = new();
        if (lightSource is PointLightSource light)
        {
            LightIntensity.LightIntensityBuilder lightIntensityBuilder2 = new();
            if (pointOfIntersection.Figure.Material.Reflection is null || recursionCount <= 0)
            {
                if (lightSource.IsInShadow(pointOfIntersection, this)) return new LightIntensity.LightIntensityBuilder();
                var Lm = new Vector3(pointOfIntersection.Position, light.Location).GetNormalized();
                var N = pointOfIntersection.Figure.GetNormal(pointOfIntersection);
                var LdotN = Lm.Dot(N);
                lightIntensityBuilder2 += light.GetIntensity(pointOfIntersection);
                lightIntensityBuilder2 *= LdotN;
                lightIntensityBuilder2 *= pointOfIntersection.Figure.Material.KDiffuse;
                var lni = lightIntensityBuilder2.Build();
                var Rm = (2 * LdotN * N - Lm).GetNormalized();
                var RmDotV = Rm.Dot(-ray.Direction);
                var powAlpha = Math.Pow(RmDotV, pointOfIntersection.Figure.Material.ShinessConstant);
                lightIntensityBuilder2 = new LightIntensity.LightIntensityBuilder();
                lightIntensityBuilder2 += light.GetIntensity(pointOfIntersection);
                lightIntensityBuilder2 *= powAlpha;
                lightIntensityBuilder2 *= pointOfIntersection.Figure.Material.KSpecular;
                var powKsIms = lightIntensityBuilder2.Build();
                lightIntensityBuilder += lni;
                lightIntensityBuilder += powKsIms;
                return lightIntensityBuilder * pointOfIntersection.Figure.GetTexture(pointOfIntersection.Position);
            }

            if (recursionCount > 0)
            {
                var reflectedRay =
                    pointOfIntersection.Figure.Material.Reflection.GetReflectedRay(ray, pointOfIntersection);
                var reflectedPointOfIntersection = Intersection(reflectedRay, pointOfIntersection.Figure);
                if (reflectedPointOfIntersection is null) return lightIntensityBuilder;
                lightIntensityBuilder2 = GetLightIntensityForLightSource(reflectedRay, reflectedPointOfIntersection,
                    lightSource, recursionCount - 1);
                lightIntensityBuilder += lightIntensityBuilder2;
            }
        }
        else
        {
            lightIntensityBuilder += lightSource.GetIntensity(pointOfIntersection) * pointOfIntersection.Figure.GetTexture(pointOfIntersection.Position);
        }

        return lightIntensityBuilder;
    }

    public void AddFigure(Figure figure)
    {
        _figures.Add(new ComplexFigure(figure));
    }

    public void AddLight(AbstractLightSource lightSource)
    {
        _lightSources.Add(new[] { lightSource });
    }

    public void AddLight(AbstractLightSource[] lightSource)
    {
        _lightSources.Add(lightSource);
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