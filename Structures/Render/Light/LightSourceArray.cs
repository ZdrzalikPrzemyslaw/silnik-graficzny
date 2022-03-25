using System.Collections;

namespace Structures.Render.Light;

public class LightSourceArray : AbstractLightSourceArray
{
    private AbstractLightSource [] _lightSource;

    public LightSourceArray(params AbstractLightSource[] lightSources)
    {
        _lightSource = lightSources;
    }

    public static implicit operator LightSourceArray(AbstractLightSource[] value)
    {
        return new LightSourceArray(value);
    }

    public static implicit operator AbstractLightSource[](LightSourceArray me)
    {
        return me._lightSource;
    }

    public override AbstractLightSource[] GetLightSources()
    {
        return _lightSource;
    }
}