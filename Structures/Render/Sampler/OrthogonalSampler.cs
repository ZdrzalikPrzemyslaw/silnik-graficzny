using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render.Sampler;

public class OrthogonalSampler : AbstractSampler
{
    public OrthogonalSampler(LightIntensity lightIntensity) : base(lightIntensity)
    {
    }

    public OrthogonalSampler()
    {
    }

    public override LightIntensity Sample(Scene scene, Ray rayLeftUp, double step, Vector3 up, int recursionLevel = 0)
    {
        var rayRightUp = new Ray(
            new Ray(rayLeftUp.Origin, rayLeftUp.Direction.Cross(up)).PointAtDistanceFromOrigin(-step),
            rayLeftUp.Direction);
        var rayRightDown = new Ray(new Ray(rayRightUp.Origin, up).PointAtDistanceFromOrigin(-step),
            rayLeftUp.Direction);
        var rayLeftDown = new Ray(new Ray(rayLeftUp.Origin, up).PointAtDistanceFromOrigin(-step),
            rayLeftUp.Direction);
        var rayCenter = new Ray(Vector3.PointBetweenTwoPoints(rayLeftDown.Origin, rayLeftUp.Origin),
            rayLeftUp.Direction);

        var intensityLeftUp = scene.GetLightIntensity(rayLeftUp);
        var intensityRightUp = scene.GetLightIntensity(rayRightUp);
        var intensityRightDown = scene.GetLightIntensity(rayRightDown);
        var intensityLeftDown = scene.GetLightIntensity(rayLeftDown);
        var intensityCenter = scene.GetLightIntensity(rayCenter);

        if (recursionLevel >= RecursionLimit)
            return intensityLeftUp / 5 + intensityLeftDown / 5 + intensityRightDown / 5 + intensityRightUp / 5 +
                   intensityCenter / 5;

        var lightIntensityRes = new LightIntensity();

        lightIntensityRes += (Math.Abs(intensityLeftUp.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityLeftUp.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityLeftUp.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityLeftUp.R + intensityCenter.R) / 2,
                (intensityLeftUp.G + intensityCenter.G) / 2,
                (intensityLeftUp.B + intensityCenter.B) / 2
            )
            : new OrthogonalSampler(SpatialContrast)
                .Sample(scene, rayLeftUp, step / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityRightUp.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityRightUp.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityRightUp.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityRightUp.R + intensityCenter.R) / 2,
                (intensityRightUp.G + intensityCenter.G) / 2,
                (intensityRightUp.B + intensityCenter.B) / 2
            )
            : new OrthogonalSampler(SpatialContrast)
                .Sample(scene,
                    new Ray(Vector3.PointBetweenTwoPoints(rayLeftUp.Origin, rayRightUp.Origin),
                        rayLeftUp.Direction),
                    step / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityRightDown.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityRightDown.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityRightDown.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityRightDown.R + intensityCenter.R) / 2,
                (intensityRightDown.G + intensityCenter.G) / 2,
                (intensityRightDown.B + intensityCenter.B) / 2
            )
            : new OrthogonalSampler(SpatialContrast)
                .Sample(scene, rayCenter, step / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityLeftDown.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityLeftDown.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityLeftDown.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityLeftDown.R + intensityCenter.R) / 2,
                (intensityLeftDown.G + intensityCenter.G) / 2,
                (intensityLeftDown.B + intensityCenter.B) / 2
            )
            : new OrthogonalSampler(SpatialContrast)
                .Sample(scene,
                    new Ray(Vector3.PointBetweenTwoPoints(rayLeftDown.Origin, rayLeftUp.Origin), rayLeftUp.Direction),
                    step / 2, up, recursionLevel + 1)) / 4;

        return lightIntensityRes;
    }
}