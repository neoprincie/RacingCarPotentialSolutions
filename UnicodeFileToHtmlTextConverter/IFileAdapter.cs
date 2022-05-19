using System.IO;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter;

public interface IFileAdapter
{
    TextReader OpenText();
    string GetFilePath();
}