using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class PointLightSource : ComplexLightSource
{
    public Vector3 Location { get; set; }
    public double A1 { get; set; } //współczynnik zanikania 
    public double A2 { get; set; } //współczynnik zanikania 

    public override Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }

    public override Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }

    public override LightIntensity GetIntensity(Vector3 position)
    {
        return Colour * (1 / ((A1 + A2) * position.Distance(Location)));
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        throw new NotImplementedException();
    }

    public PointLightSource(LightIntensity colour, double a1, double a2) : base(colour)
    {
        A1 = a1;
        A2 = a2;
    }
}