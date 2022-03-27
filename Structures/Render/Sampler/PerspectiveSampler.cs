using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render.Sampler;

public class PerspectiveSampler : AbstractSampler
{
    public PerspectiveSampler(LightIntensity lightIntensity) : base(lightIntensity)
    {
    }

    public PerspectiveSampler()
    {
    }


    public override LightIntensity Sample(Scene scene, Ray rayLeftUp, double stepX, double stepY, Vector3 up,
        int recursionLevel = 0)
    {
        var matrixRight = Matrix.Rotate(stepX * Math.PI / 180, up);
        var matrixDown = Matrix.Rotate(-stepY * Math.PI / 180, rayLeftUp.Direction.Cross(up));
        var matrixHalfRight = Matrix.Rotate(stepX / 2 * Math.PI / 180, up);
        var matrixHalfDown = Matrix.Rotate(-stepY / 2 * Math.PI / 180, rayLeftUp.Direction.Cross(up));
        var rayRightUp = rayLeftUp.Rotate(matrixRight);
        var rayRightDown = rayLeftUp.Rotate(matrixRight * matrixDown);
        var rayLeftDown = rayLeftUp.Rotate(matrixDown);
        var rayCenter = rayLeftUp.Rotate(matrixHalfRight * matrixHalfDown);

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
            : new PerspectiveSampler(SpatialContrast)
                .Sample(scene, rayLeftUp, stepX / 2, stepY / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityRightUp.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityRightUp.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityRightUp.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityRightUp.R + intensityCenter.R) / 2,
                (intensityRightUp.G + intensityCenter.G) / 2,
                (intensityRightUp.B + intensityCenter.B) / 2
            )
            : new PerspectiveSampler(SpatialContrast)
                .Sample(scene, rayLeftUp.Rotate(matrixHalfRight),
                    stepX / 2, stepY / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityRightDown.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityRightDown.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityRightDown.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityRightDown.R + intensityCenter.R) / 2,
                (intensityRightDown.G + intensityCenter.G) / 2,
                (intensityRightDown.B + intensityCenter.B) / 2
            )
            : new PerspectiveSampler(SpatialContrast)
                .Sample(scene, rayCenter, stepX / 2, stepY / 2, up, recursionLevel + 1)) / 4;

        lightIntensityRes += (Math.Abs(intensityLeftDown.R - intensityCenter.R) < SpatialContrast.R &&
                              Math.Abs(intensityLeftDown.G - intensityCenter.G) < SpatialContrast.G &&
                              Math.Abs(intensityLeftDown.B - intensityCenter.B) < SpatialContrast.B
            ? new LightIntensity(
                (intensityLeftDown.R + intensityCenter.R) / 2,
                (intensityLeftDown.G + intensityCenter.G) / 2,
                (intensityLeftDown.B + intensityCenter.B) / 2
            )
            : new PerspectiveSampler(SpatialContrast)
                .Sample(scene, rayLeftUp.Rotate(matrixHalfDown),
                    stepX / 2, stepY / 2, up, recursionLevel + 1)) / 4;

        return lightIntensityRes;
    }
}