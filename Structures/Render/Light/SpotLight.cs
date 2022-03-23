using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class SpotLight : ComplexLightSource
{
    public Vector3 Location { get; set; }
    public double A1 { get; set; } //współczynnik zanikania 
    public double A2 { get; set; } //współczynnik zanikania 
    public double Ca { get; set; } //współczynnik koncentracji natężenia światła
    public double Theta { get; set; } //kąt rozwarcia promieni światła
    
    public SpotLight(LightIntensity colour, Vector3 location, double a1, double a2, double ca, double theta) : base(colour)
    {
        Location = location;
        A1 = a1;
        A2 = a2;
        Ca = ca;
        Theta = theta;
    }

    public override LightIntensity GetIntensity(Vector3 position)
    {
        var meter = Location.Dot(position - Location) / position.Distance(Location);
        return Colour * Math.Pow(meter, Ca) / ((A1 + A2) * position.Distance(Location));
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        throw new NotImplementedException();
    }

    public override Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }

    public override Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }
}