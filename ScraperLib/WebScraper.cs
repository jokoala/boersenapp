using System;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace ScraperLib
{
	public class WebScraper
	{
		public string RawData { get; set; }
		public string Url { get; set; }
		public HtmlDocument Document { get; set; }

		public void ReadData ()
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create (Url);
			req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse ();

			Stream dataStream = resp.GetResponseStream ();
			StreamReader dataReader = new StreamReader(dataStream);
			RawData = dataReader.ReadToEnd ();
		}

		public void ParseData()
		{
			Document = new HtmlDocument();
			Document.LoadHtml (RawData);
		}
	}
}

