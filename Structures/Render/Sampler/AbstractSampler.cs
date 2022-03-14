using Structures.MathObjects;

namespace Structures.Render.Sampler;

public abstract class AbstractSampler : ISampler
{
    public abstract LightIntensity Sample(Scene scene, Ray ray, double step, Vector3 up);

    public LightIntensity SpatialContrast { get; }

    public AbstractSampler(LightIntensity lightIntensity)
    {
        SpatialContrast = lightIntensity;
    }

    public AbstractSampler() : this(new LightIntensity(0.05, 0.05, 0.05))
    {
    }
}