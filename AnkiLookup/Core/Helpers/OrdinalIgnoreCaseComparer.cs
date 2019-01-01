using System.Collections.Generic;
using System.Globalization;

namespace AnkiLookup.UI.Forms
{
    public class OrdinalIgnoreCaseComparer : Comparer<string>
    {
        private readonly CompareInfo _compareInfo;

        public OrdinalIgnoreCaseComparer()
        {
            _compareInfo = CompareInfo.GetCompareInfo(CultureInfo.InvariantCulture.Name);
        }

        public override int Compare(string x, string y)
        {
            return _compareInfo.Compare(x, y, CompareOptions.OrdinalIgnoreCase);
        }
    }
}