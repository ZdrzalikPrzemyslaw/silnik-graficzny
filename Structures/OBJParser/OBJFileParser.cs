using System.Globalization;
using Structures.Figures;
using Structures.MathObjects;

namespace Structures.OBJParser;

public class OBJFileParser : IFileParser<ComplexFigure>
{
    public List<ComplexFigure> ParseFile(params string[] filePaths)
    {
        return ParseFile(Vector3.Zero(), filePaths);
    }
 
    public List<ComplexFigure> ParseDirectory(string dirPath)
    {
        throw new NotImplementedException();
    }

    public List<ComplexFigure> ParseFile(string filePath)
    {
        return ParseFile(Vector3.Zero(), filePath);
    }

    public List<ComplexFigure> ParseFile(Vector3 translation, string filePath)
    {
        var complexFigures = new List<ComplexFigure>();
        ComplexFigureBuilder complexFigureBuilder = null;
        foreach (var line in File.ReadLines(filePath))
        {
            var phrases = line.Trim(' ', '\n').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (phrases.Length == 0) continue;
            switch (phrases[0])
            {
                case "g":
                    if (complexFigureBuilder is not null) complexFigures.Add(complexFigureBuilder.Build(translation));
                    complexFigureBuilder = new ComplexFigureBuilder();
                    complexFigureBuilder.SetName(phrases[1]);
                    break;
                case "v":
                    complexFigureBuilder.AddV(new Vector3(double.Parse(phrases[1], CultureInfo.InvariantCulture),
                        double.Parse(phrases[2], CultureInfo.InvariantCulture),
                        double.Parse(phrases[3], CultureInfo.InvariantCulture)));
                    break;
                case "vn":
                    complexFigureBuilder.AddVn(new Vector3(double.Parse(phrases[1], CultureInfo.InvariantCulture),
                        double.Parse(phrases[2], CultureInfo.InvariantCulture),
                        double.Parse(phrases[3], CultureInfo.InvariantCulture)));
                    break;
                case "f":
                    complexFigureBuilder.AddF(phrases[1], phrases[2], phrases[3]);
                    break;
                case "#":
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        if (complexFigureBuilder is not null) complexFigures.Add(complexFigureBuilder.Build(translation));
        return complexFigures;
    }

    public List<ComplexFigure> ParseFile(Vector3 translation, params string[] filePaths)
    {
        List<ComplexFigure> complexFigures = new();
        foreach (var filePath in filePaths) complexFigures.AddRange(ParseFile(translation, filePath));

        return complexFigures;
    }
}