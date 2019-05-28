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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !fileName.Contains(".exe"))
                fileName += ".exe";

            return File.Exists($"{directory}{Path.DirectorySeparatorChar}{fileName}");
        }
    }
}
