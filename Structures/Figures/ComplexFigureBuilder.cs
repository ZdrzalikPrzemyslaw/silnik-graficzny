using System.Collections.Specialized;
using Structures.MathObjects;
using Structures.Render;

namespace Structures.Figures;

public class ComplexFigureBuilder
{
    private List<Vector3> _v = new();
    private List<Vector3> _vn = new();
    private List<string[]> _f = new();
    private string _name = "";

    public ComplexFigure Build(Vector3 transaltion = null)
    {
        var figures = new List<SimpleFigure>();
        if (transaltion is not null)
        {
            List<Vector3> list = new();
            foreach (var v in _v)
            {
                list.Add(v + transaltion);
            }

            _v = list;
        }
        foreach (var fDataLine in _f)
        {
            var oneOfF = fDataLine[0].Split("//");
            var pointA = _v[int.Parse(oneOfF[0]) - 1];
            oneOfF = fDataLine[1].Split("//");
            var pointB = _v[int.Parse(oneOfF[0]) - 1];
            oneOfF = fDataLine[2].Split("//");
            var pointC = _v[int.Parse(oneOfF[0]) - 1];

            var normal = _vn[int.Parse(oneOfF[1]) - 1];

            figures.Add(new Triangle(normal, pointA, LightIntensity.DefaultObject(), pointA, pointB, pointC));
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