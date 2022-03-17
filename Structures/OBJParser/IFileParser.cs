namespace Structures.OBJParser;

public interface IFileParser<T>
{
    List<T> ParseFile(params string[] filePath);
    List<T> ParseDirectory(string dirPath);
}