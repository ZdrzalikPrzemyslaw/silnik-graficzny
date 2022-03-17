using Structures.Figures;
using Structures.MathObjects;

namespace Structures.OBJParser;

public class OBJFileParser : IFileParser<ComplexFigure>
{
    // TODO:
    public List<ComplexFigure> ParseFile(params string[] filePath)
    {
        // Lista wierzcholkow
        // Lista Normalnych, ktore trzeba znormalizowac
        // Z nich lista trójkątów (figur)
        // Z listy figur ComplexFigure
        // W jednym pliku moze byc wiecej niz jedna ComplexFigure dlatego lista
        // Jak bedzie trzeba cos wiecej to dodamy
        // http://cs.wellesley.edu/~cs307/readings/obj-ojects.html <- mysle ze to wszystko wyjasia,
        // skopiuj ten example file i bedziemy go uzywac powinien on zrobic Cube 1 na 1 na 1.
        throw new NotImplementedException();
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