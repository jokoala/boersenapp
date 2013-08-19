using System;
using NUnit.Framework;

using HtmlAgilityPack;

using ScraperLib;

namespace UnitTests
{
	[TestFixture]
	public class WebScraperTest
	{
		WebScraper webscraper;

		public WebScraperTest ()
		{
			webscraper = new WebScraper ();
		}

		[Test()]
		public void ReadData_FromLocalServer_ReturnsFileContents ()
		{
			webscraper.Url = "http://localhost/~johannes/boersenapp/test.html";
			webscraper.ReadData();

			Assert.AreEqual ("It works!\n", webscraper.RawData);
		}

		[Test]
		public void ParseData_FromKnownHtml_ParsesHtml ()
		{
			webscraper.RawData = "<html><title><head>Demo</head></title><body><p>Test</p></body></html>";
			webscraper.ParseData ();

			HtmlNode head = webscraper.Document.DocumentNode.SelectSingleNode ("//title/head");
			Assert.AreEqual ("Demo", head.InnerText);
		}
	}
}

