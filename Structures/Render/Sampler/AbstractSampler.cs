using Structures.MathObjects;
using Structures.Render.Light;

namespace Structures.Render.Sampler;

public abstract class AbstractSampler : ISampler
{
    private static readonly LightIntensity DefaultLightIntensity = new(0.05, 0.05, 0.05);
    private static readonly int _defaultRecursionLimit = 2;

    public AbstractSampler(LightIntensity lightIntensity, int recursionLimit)
    {
        RecursionLimit = recursionLimit;
        SpatialContrast = lightIntensity;
    }

    public AbstractSampler(int recursionLimit) : this(DefaultLightIntensity, _defaultRecursionLimit)
    {
    }


    public AbstractSampler(LightIntensity lightIntensity) : this(lightIntensity, _defaultRecursionLimit)
    {
    }

    public AbstractSampler() : this(DefaultLightIntensity)
    {
    }

    public int RecursionLimit { get; }

    public LightIntensity SpatialContrast { get; }

    public abstract LightIntensity Sample(Scene scene, Ray rayLeftUp, double stepX, double stepY, Vector3 up,
        int recursionLevel = 0);
}