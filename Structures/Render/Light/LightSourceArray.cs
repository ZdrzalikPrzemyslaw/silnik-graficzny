using System.Collections;

namespace Structures.Render.Light;

public class LightSourceArray : AbstractLightSourceArray
{
    private LightSource [] _lightSource;

    public LightSourceArray(params LightSource[] lightSources)
    {
        _lightSource = lightSources;
    }

    public static implicit operator LightSourceArray(LightSource[] value)
    {
        return new LightSourceArray(value);
    }

    public static implicit operator LightSource[](LightSourceArray me)
    {
        return me._lightSource;
    }

    protected override LightSource[] GetLightSources()
    {
        return _lightSource;
    }
}