using System.Collections;

namespace Structures.Render.Light;

public abstract class AbstractLightSourceArray : IEnumerable<AbstractLightSource>
{
    public abstract AbstractLightSource[] GetLightSources();
    public ref AbstractLightSource this[int row] => ref GetLightSources()[row];

    public IEnumerator<AbstractLightSource> GetEnumerator()
    {
        return ((IEnumerable<AbstractLightSource>) GetLightSources()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}