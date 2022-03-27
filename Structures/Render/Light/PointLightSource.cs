using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class PointLightSource : ComplexLightSource
{
    public PointLightSource(LightIntensity colour, Vector3 location, double constAttenuation, double linearAttenuation)
        : base(colour)
    {
        Location = location;
        ConstAttenuation = constAttenuation;
        LinearAttenuation = linearAttenuation;
    }

    public PointLightSource(PointLightSource pointLightSource, Vector3 location) : base(pointLightSource.Colour)
    {
        Location = location;
        ConstAttenuation = pointLightSource.ConstAttenuation;
        LinearAttenuation = pointLightSource.LinearAttenuation;
    }

    public Vector3 Location { get; set; }
    public double ConstAttenuation { get; set; } //współczynnik zanikania 
    public double LinearAttenuation { get; set; } //współczynnik zanikania 

    public override LightIntensity GetIntensity(Vector3 position)
    {
        return GetIntensity(new PointOfIntersection(null, position));
    }

    public override LightIntensity GetIntensity(PointOfIntersection point)
    {
        return Colour * (1 / (ConstAttenuation + LinearAttenuation * point.Position.Distance(Location)));
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        if (Location == pointOfIntersection.Position) return false;

        var ray = new Ray(Location, new Vector3(Location, pointOfIntersection.Position));
        var distance = Location.Distance(pointOfIntersection.Position);
        foreach (var complexFigure in scene.GetReadOnlyFiguresList())
            try
            {
                var intersection = complexFigure.Intersection(ray);
                if (intersection is null) continue;
                if (distance - intersection.Position.Distance(Location) > 0.0001) return true;
            }
            catch (Plane.InfiniteIntersectionsException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

        return false;
    }
}