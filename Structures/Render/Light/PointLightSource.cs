using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class PointLightSource : ComplexLightSource
{
    public Vector3 location { get; set; }
    
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
        throw new NotImplementedException();
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        throw new NotImplementedException();
    }

    public PointLightSource(LightIntensity colour) : base(colour)
    {
    }
}