using System.IO;
using System.Runtime.InteropServices;

namespace CrabsWave.Utils.IO
{
    public static class FolderUtils
    {
        public static string GetAbsolutePath() => Directory.GetCurrentDirectory();

        public static bool SafeCheckExecutableExists(string fileName, string directory = "")
        {
            if (string.IsNullOrWhiteSpace(directory))
                return File.Exists(GetCurrentPathWithDriverFileName(fileName));

            return File.Exists($"{directory}{Path.DirectorySeparatorChar}{GetFileNameFromOs(fileName)}");
        }

        public static string GetCurrentPathWithDriverFileName(string fileName) => $"{GetAbsolutePath()}{Path.DirectorySeparatorChar}{GetFileNameFromOs(fileName)}";

        public static string GetFileNameFromOs(string fileName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !fileName.Contains(".exe"))
                fileName += ".exe";

            return fileName;
        }

        public static string ReplaceInvalidFileNameChars(string fileName) => string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
    }
}
