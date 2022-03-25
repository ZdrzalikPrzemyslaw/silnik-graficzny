using Structures.MathObjects;

namespace Structures.Figures;

public class ComplexFigureBuilder
{
    private readonly List<string[]> _f = new();
    private readonly List<Vector3> _vn = new();
    private string _name = "";
    private List<Vector3> _v = new();

    public ComplexFigure Build(Vector3 transaltion = null)
    {
        var figures = new List<SimpleFigure>();
        if (transaltion is not null)
        {
            List<Vector3> list = new();
            foreach (var v in _v) list.Add(v + transaltion);

            _v = list;
        }

        foreach (var fDataLine in _f)
            if (fDataLine[0].Contains("//"))
            {
                var oneOfF = fDataLine[0].Split("//");
                var pointA = _v[int.Parse(oneOfF[0]) - 1];
                oneOfF = fDataLine[1].Split("//");
                var pointB = _v[int.Parse(oneOfF[0]) - 1];
                oneOfF = fDataLine[2].Split("//");
                var pointC = _v[int.Parse(oneOfF[0]) - 1];

                var normal = _vn[int.Parse(oneOfF[1]) - 1];
                figures.Add(new Triangle(pointA, pointB, pointC));
            }
            else
            {
                var pointA = _v[int.Parse(fDataLine[0]) - 1];
                var pointB = _v[int.Parse(fDataLine[1]) - 1];
                var pointC = _v[int.Parse(fDataLine[2]) - 1];

                figures.Add(new Triangle(pointA, pointB, pointC));
            }


        return new ComplexFigure(_name, figures.ToArray());
    }

    public ComplexFigureBuilder AddV(Vector3 v)
    {
        _v.Add(v);
        return this;
    }

    public ComplexFigureBuilder AddVn(Vector3 vn)
    {
        _vn.Add(vn.GetNormalized());
        return this;
    }

    public ComplexFigureBuilder AddF(params string[] f)
    {
        _f.Add(f);
        return this;
    }

    public ComplexFigureBuilder SetName(string name)
    {
        _name = name;
        return this;
    }
}