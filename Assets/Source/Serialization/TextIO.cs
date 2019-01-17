using System.IO;

namespace Lomztein.ProjectAI.Serialization
{
    public static class TextIO
    {
        public static void Save (string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public static string Load(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
