using System.Resources;
using Structures.MathObjects;

namespace Structures.Render.Sampler;

public class OrthogonalSampler : AbstractSampler
{
    public override LightIntensity Sample(Scene scene, Ray ray, double step, Vector3 up)
    {
        Ray rayLeftUp = ray;
        Ray rayRightUp = new Ray(new Ray(ray.Origin, ray.Direction.Cross(up)).PointAtDistanceFromOrigin(-step),
            ray.Direction);
        Ray rayRightDown = new Ray(new Ray(rayRightUp.Origin, up).PointAtDistanceFromOrigin(-step), rayRightUp.Direction);
        Ray rayLeftDown = new Ray(new Ray(ray.Origin, up).PointAtDistanceFromOrigin(-step), ray.Direction);
        Ray rayCenter = new Ray(rayRightDown.Origin - rayLeftUp.Origin, ray.Direction);
        
        
    }
}