using Structures.MathObjects;

namespace Structures.Render.Sampler;

public interface ISampler
{
    public LightIntensity Sample(Scene scene, Ray ray, double step, Vector3 up);
}