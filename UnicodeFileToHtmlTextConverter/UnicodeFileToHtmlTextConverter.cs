using System.IO;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverter
    {
        private readonly IFileAdapter _fileAdapter;

        public UnicodeFileToHtmlTextConverter(IFileAdapter fileAdapter)
        {
            _fileAdapter = fileAdapter;
        }

        public string GetFilename()
        {
            return _fileAdapter.GetFilePath();
        }

        public string ConvertToHtml()
        {
            using var unicodeFileStream = _fileAdapter.OpenText();
            var html = string.Empty;

            var line = unicodeFileStream.ReadLine();
            while (line != null)
            {
                html += HttpUtility.HtmlEncode(line);
                html += "<br />";
                line = unicodeFileStream.ReadLine();
            }

            return html;
        }
    }
    class HttpUtility
    {
        public static string HtmlEncode(string line)
        {
            line = line.Replace("&", "&amp;");
            line = line.Replace("<", "&lt;");
            line = line.Replace(">", "&gt;");
            line = line.Replace("\"", "&quot;");
            line = line.Replace("\'", "&quot;");
            return line;
        }
    }
}
