using System.Text;
using Structures.Figures;
using Structures.MathObjects;

namespace Structures.OBJParser;

public class OBJFileParser : IFileParser<ComplexFigure>
{
    // TODO:
    public List<ComplexFigure> ParseFile(params string[] filePaths)
    {
        var complexFigures = new List<ComplexFigure>();
        foreach (var filePath in filePaths) complexFigures.AddRange(ParseFile(filePath));
        // Lista wierzcholkow
        // Lista Normalnych, ktore trzeba znormalizowac
        // Z nich lista trójkątów (figur)
        // Z listy figur ComplexFigure
        // W jednym pliku moze byc wiecej niz jedna ComplexFigure dlatego lista
        // Jak bedzie trzeba cos wiecej to dodamy
        // http://cs.wellesley.edu/~cs307/readings/obj-ojects.html <- mysle ze to wszystko wyjasia,
        // skopiuj ten example file i bedziemy go uzywac powinien on zrobic Cube 1 na 1 na 1.
        return complexFigures;
    }

    public List<ComplexFigure> ParseFile(string filePath)
    {
        var complexFigures = new List<ComplexFigure>();
        ComplexFigureBuilder complexFigureBuilder = null;
        foreach (var line in File.ReadLines(filePath))
        {
            var phrases = line.Split(' ');
            if (phrases.Length == 0) continue;
            switch (phrases[0])
            {
                case "g":
                    complexFigureBuilder = new ComplexFigureBuilder();
                    break;
                case "v":
                    complexFigureBuilder.AddV(new Vector3(double.Parse(phrases[1]), double.Parse(phrases[2]),
                        double.Parse(phrases[3])));
                    break;
                case "vn":
                    complexFigureBuilder.AddVn(new Vector3(double.Parse(phrases[1]), double.Parse(phrases[2]),
                        double.Parse(phrases[3])));
                    break;
                case "f":
                    complexFigureBuilder.AddF(phrases[1], phrases[2], phrases[3]);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }


    // TODO: wydaje mi sie spoko zeby mozna bylo ustawic od razu przesuniecie dla wszystkich generowanych figur
    public List<ComplexFigure> ParseFile(Vector3 basePosition, params string[] filePath)
    {
        throw new NotImplementedException();
    }

    public List<ComplexFigure> ParseDirectory(string dirPath)
    {
        throw new NotImplementedException();
    }
}