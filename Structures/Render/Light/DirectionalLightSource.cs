using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class DirectionalLightSource : ComplexLightSource
{
    public Vector3 Direction { get; set; }
    
    public DirectionalLightSource(LightIntensity lightIntensity): this (lightIntensity, Vector3.Down())
    {
    }

    public DirectionalLightSource(LightIntensity lightIntensity, Vector3 direction) : base(lightIntensity)
    {
        Direction = direction.GetNormalized();
    }

    public DirectionalLightSource() : this(new LightIntensity())
    {
    }

    public override LightIntensity GetIntensity(Vector3 position)
    {
        return Colour; // czy tutaj nie zwracamy koloru tylko wtedy kiedy nie jest w cieniu?
    }

    public override LightIntensity GetIntensity(PointOfIntersection point)
    {
        return Colour; // czy tutaj nie zwracamy koloru tylko wtedy kiedy nie jest w cieniu?
    }

    public override Vector3 GetDiffuse(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }

    public override Vector3 GetSpecular(Vector3 cameraPosition, PointOfIntersection pointOfIntersection)
    {
        throw new NotImplementedException();
    }

    public override bool IsInShadow(PointOfIntersection pointOfIntersection, Scene scene)
    {
        throw new NotImplementedException();
    }
}