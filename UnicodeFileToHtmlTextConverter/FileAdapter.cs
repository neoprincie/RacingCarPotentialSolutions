using System.IO;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter;

public class FileAdapter : IFileAdapter
{
    private readonly string _filePath;
    
    public FileAdapter(string filePath)
    {
        _filePath = filePath;
    }
    
    public TextReader OpenText()
    {
        return File.OpenText(_filePath);
    }

    public string GetFilePath()
    {
        return _filePath;
    }
}