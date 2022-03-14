using Structures.MathObjects;

namespace Structures.Render.Sampler;

public class PerspectiveSampler : AbstractSampler
{
    public override LightIntensity Sample(Scene scene, Ray rayLeftUp, double step, Vector3 up, int recursionLevel = 0)
    {
        throw new NotImplementedException();
    }
}