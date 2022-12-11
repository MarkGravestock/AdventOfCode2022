namespace Common;

public class FileReader
{
    private readonly string fileName;

    public FileReader(string fileName)
    {
        this.fileName = fileName;
    }

    public IEnumerable<string> Lines()
    {
        using var sr = new StreamReader(fileName);
        var text = sr.ReadToEnd();
        var lines = text.Split("\n");
        return lines.Select(x => x.Trim()).Where(x => x != String.Empty);
    }
}