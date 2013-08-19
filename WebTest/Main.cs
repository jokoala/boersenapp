using System;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace WebTest
{
	class WebScraper
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
	class MainClass
	{
		public static void web_test1 ()
		{
			WebClient client = new WebClient();
			client.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

			string s = client.DownloadString ("http://localhost");
			//Stream data = client.OpenRead ("http://localhost");
			//StreamReader reader = new StreamReader(data);
			//string s = reader.ReadToEnd ();

			Console.WriteLine (s);

			//data.Close ();
			//reader.Close ();
			client.Dispose ();
		}

		public static void web_test2 ()
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create ("http://localhost");
			req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse ();

			Console.WriteLine (resp.StatusDescription);

			Console.WriteLine (resp.StatusCode);
			Stream dataStream = resp.GetResponseStream ();
			StreamReader dataReader = new StreamReader (dataStream);
			string s = dataReader.ReadToEnd ();
			dataReader.Close ();
			resp.Close ();

			Console.WriteLine (s);
		}

		public static void Main (string[] args)
		{
			WebScraper webscraper = new WebScraper ();

			webscraper.Url = "http://localhost/~johannes/boersenapp/DE000GT443X6.html";
			webscraper.ReadData ();
			webscraper.ParseData ();

			foreach (HtmlNode link in webscraper.Document.DocumentNode.SelectNodes ("//a[@href]")) {
				Console.WriteLine (link.Attributes["href"].Value);
			}
		}
	}
}
