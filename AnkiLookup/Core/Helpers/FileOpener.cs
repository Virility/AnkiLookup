using System;
using System.Diagnostics;
using System.IO;

namespace AnkiLookup.Core.Helpers
{
    public static class FileOpener
    {
        public static void Open(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            var startInfo = new ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = string.Format("/e, /select, \"{0}\"", filePath)
            };
            Process.Start(startInfo);
        }
    }
}
