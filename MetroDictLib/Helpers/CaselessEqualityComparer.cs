using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MetroDictLib.Helpers
{
	internal class CaselessEqualityComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return (string.Compare(x, y, true, Thread.CurrentThread.CurrentCulture) == 0);
		}

		public int GetHashCode(string obj)
		{
			return obj.GetHashCode();
		}
	}
}
