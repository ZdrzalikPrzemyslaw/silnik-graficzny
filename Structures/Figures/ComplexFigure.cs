using Structures.MathObjects;

namespace Structures.Figures;

public class ComplexFigure : AbstractFigureList<SimpleFigure>
{
    private readonly List<SimpleFigure> _figures = new();

    public string Name { get; }

    public ComplexFigure() : this(string.Empty)
    {
    }

    public ComplexFigure(string name) : this(Array.Empty<SimpleFigure>(), name)
    {
    }

    public ComplexFigure(IEnumerable<SimpleFigure?> simpleFigures) : this(simpleFigures.Where(i => i is not null)
        .Cast<SimpleFigure>()
        .ToArray())
    {
    }

    public ComplexFigure(params SimpleFigure[] figures) : this(figures, string.Empty)
    {
    }

    public ComplexFigure(string name, params SimpleFigure[] figures) : this(figures, name)
    {
    }

    public ComplexFigure(IEnumerable<SimpleFigure> figures, string name)
    {
        _figures.AddRange(figures);
        Name = name;
    }

    public ComplexFigure(SimpleFigure figure, string name) : this(new[] {figure}, name)
    {
    }

    protected override List<SimpleFigure> GetList()
    {
        return _figures;
    }
}