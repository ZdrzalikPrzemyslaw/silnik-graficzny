using Structures.MathObjects;

namespace Structures.Figures;

public class ComplexFigure : AbstractFigureList<Figure>
{
    private readonly List<Figure> _figures = new();

    public ComplexFigure() : this(string.Empty)
    {
    }

    public ComplexFigure(string name) : this(Array.Empty<Figure>(), name)
    {
    }

    public ComplexFigure(IEnumerable<Figure?> Figures) : this(Figures.Where(i => i is not null)
        .Cast<Figure>()
        .ToArray())
    {
    }

    public ComplexFigure(params Figure[] figures) : this(figures, string.Empty)
    {
    }

    public ComplexFigure(string name, params Figure[] figures) : this(figures, name)
    {
    }

    public ComplexFigure(IEnumerable<Figure> figures, string name)
    {
        _figures.AddRange(figures);
        Name = name;
    }

    public ComplexFigure(Figure figure, string name) : this(new[] { figure }, name)
    {
    }

    public string Name { get; }

    protected override List<Figure> GetList()
    {
        return _figures;
    }
}