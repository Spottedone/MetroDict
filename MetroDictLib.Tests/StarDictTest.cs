using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetroDictLib.Tests
{
	[TestClass]
	public class StarDictTest
	{
		[TestMethod]
		public void Init_Success()
		{
			var workDir = @"C:\Projects\_POC\MetroDict\MetroDictLib.Tests\bin\Debug\TestData";
			var sd = new StarDict(workDir, "german_rus");

			sd.Init().Wait();
		}

		[TestMethod]
		[ExpectedException(typeof(AggregateException))]
		public void Init_Failure()
		{
			var workDir = @"C:\Projects\_POC\MetroDict\MetroDictLib.Tests\bin\Debug\TestData";
			var sd = new StarDict(workDir, "some_nonexistent");

			sd.Init().Wait();
		}

		[TestMethod]
		public void GetArticle_Success()
		{
			var workDir = @"C:\Projects\_POC\MetroDict\MetroDictLib.Tests\bin\Debug\TestData";
			var sd = new StarDict(workDir, "german_rus");
			sd.Init().Wait();
			
			string article = sd.GetArticle("heute").Result;
			Assert.IsNotNull(article);
			Assert.AreNotEqual(article, "");
		}

		[TestMethod]
		public void GetArticlesContaining_Success()
		{
			var workDir = @"C:\Projects\_POC\MetroDict\MetroDictLib.Tests\bin\Debug\TestData";
			var sd = new StarDict(workDir, "german_rus");
			sd.Init().Wait();

			List<string> articles = sd.GetArticlesContaining("heute");
			Assert.IsNotNull(articles);
			Assert.AreNotEqual(articles.Count, 0);
		}
	}
}
