using System.Collections;

namespace Structures.Render.Light;

public abstract class AbstractLightSourceArray : IEnumerable<LightSource>
{
    protected abstract LightSource[] GetLightSources();
    public ref LightSource this[int row] => ref GetLightSources()[row];
    public IEnumerator<LightSource> GetEnumerator()
    {
        return ((IEnumerable<LightSource>)GetLightSources()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}