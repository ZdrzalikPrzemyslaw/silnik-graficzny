using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render.Sampler;

public interface ISampler
{
    // todo: step na osi X i Y;
    public LightIntensity Sample(Scene scene, Ray rayLeftUp, double step, Vector3 up, int recursionLevel = 0);
}