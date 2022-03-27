using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class SpotLight : PointLightSource
{
    public SpotLight(LightIntensity colour, Vector3 location, double constAttenuation, double linearAttenuation,
        double dropOffRate, double cutOffAngle) : base(
        colour, location, constAttenuation, linearAttenuation)
    {
        DropOffRate = dropOffRate;
        CutOffAngle = cutOffAngle;
    }

    public double DropOffRate { get; set; } //współczynnik koncentracji natężenia światła
    public double CutOffAngle { get; set; } //kąt rozwarcia promieni światła

    public override LightIntensity GetIntensity(Vector3 position)
    {
        var meter = Location.Dot(position - Location) / position.Distance(Location);
        return Colour * Math.Pow(meter, DropOffRate) /
               ((ConstAttenuation + LinearAttenuation) * position.Distance(Location));
    }

    public override LightIntensity GetIntensity(PointOfIntersection point)
    {
        var meter = Location.Dot(point.Position - Location) / point.Position.Distance(Location);
        return Colour * Math.Pow(meter, DropOffRate) /
               ((ConstAttenuation + LinearAttenuation) * point.Position.Distance(Location));
    }
}