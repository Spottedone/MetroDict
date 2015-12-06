using System;
using System.Collections.Generic;

namespace MetroDictLib.Helpers
{
    internal class CaselessEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Compare(x, y, StringComparison.CurrentCulture) == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}