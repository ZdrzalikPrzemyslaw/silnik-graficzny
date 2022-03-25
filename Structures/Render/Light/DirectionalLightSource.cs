using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class DirectionalLightSource : ComplexLightSource
{
    public DirectionalLightSource(LightIntensity lightIntensity) : this(lightIntensity, Vector3.Down())
    {
    }

    public DirectionalLightSource(LightIntensity lightIntensity, Vector3 direction) : base(lightIntensity)
    {
        Direction = direction.GetNormalized();
    }

    public DirectionalLightSource() : this(new LightIntensity())
    {
    }

    public Vector3 Direction { get; set; }

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
        var ray = new Ray(pointOfIntersection.Position, -Direction);
        foreach (var complexFigure in scene.GetReadOnlyFiguresList())
        {
            var intersection = complexFigure.Intersection(ray);
            if (intersection is null) continue;
            if (intersection.Position == pointOfIntersection.Position) continue;
            return true;
        }

        return false;
    }
}