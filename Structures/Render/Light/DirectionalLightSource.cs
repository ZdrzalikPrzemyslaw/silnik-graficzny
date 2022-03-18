using Structures.Figures;
using Structures.MathObjects;

namespace Structures.Render.Light;

public class DirectionalLightSource : LightSource
{
    public Vector3 Direction { get; set; }

    public DirectionalLightSource(Vector3 direction)
    {
        Direction = direction.GetNormalized();
    }

    public DirectionalLightSource() : this(Vector3.Down())
    {
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