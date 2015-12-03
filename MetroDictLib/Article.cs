using System;
using System.Linq;

namespace MetroDictLib
{
	public class Article
	{
		public string Word { get; private set; }
		public UInt32 Start { get; private set; }
		public UInt32 Length { get; private set; }

		public Article(string word, UInt32 start, UInt32 length)
		{
			this.Word = word;
			this.Start = start;
			this.Length = length;
		}
	}
}
