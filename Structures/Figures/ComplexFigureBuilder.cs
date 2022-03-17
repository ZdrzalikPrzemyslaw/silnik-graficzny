using Structures.MathObjects;

namespace Structures.Figures;

public class ComplexFigureBuilder
{
    private readonly List<Vector3> _v = new();
    private readonly List<Vector3> _vn = new();
    private readonly List<string[]> _f = new();

    public ComplexFigure Build()
    {
        throw new NotImplementedException();
    }

    public ComplexFigureBuilder AddV(Vector3 v)
    {
        _v.Add(v);
        return this;
    }

    public ComplexFigureBuilder AddVn(Vector3 vn)
    {
        _vn.Add(vn);
        return this;
    }

    public ComplexFigureBuilder AddF(params string[] f)
    {
        _f.Add(f);
        return this;
    }
}