using System.Runtime.InteropServices;
using System.Text;

namespace AnkiSpanishDictWordOfTheDay.Core.Models
{
    public class IniFile
    {
        /// <summary>
        /// Initialization file.
        /// </summary>
        public string Path { get; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Set location for the initialization file path.
        /// </summary>
        /// <param name="path">INI file path.</param>
        public IniFile(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Set a value for a section->key.
        ///[section]
        ///key=value
        /// </summary>
        /// <param name="section">Section to set value to.</param>
        /// <param name="key">Key to set value to.</param>
        /// <param name="value">Value to set to section->key.</param>
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        /// <summary>
        /// Read a value from a section->key.
        /// </summary>
        /// <param name="section">Section to get value from.</param>
        /// <param name="key">Key to get value from.</param>
        /// <returns></returns>
        public string IniReadValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, string.Empty, temp, 255, Path);
            return temp.ToString();
        }
    }
}