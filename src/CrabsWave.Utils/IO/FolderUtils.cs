using System.IO;
using System.Runtime.InteropServices;

namespace CrabsWave.Utils.IO
{
    public static class FolderUtils
    {
        public static string GetAbsolutePath() => Directory.GetCurrentDirectory();

        public static bool SafeCheckFileExist(string fileName, string directory = "")
        {
            if (string.IsNullOrWhiteSpace(directory))
                directory = GetAbsolutePath();

            var extension = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                extension = ".exe";

            if (fileName.IndexOf(extension) >= 0)
                return File.Exists($"{directory}{Path.DirectorySeparatorChar}{fileName}");

            return File.Exists($"{directory}{Path.DirectorySeparatorChar}{fileName}{extension}");
        }
    }
}
