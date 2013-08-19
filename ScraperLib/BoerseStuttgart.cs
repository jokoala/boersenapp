using System;
using System.Text.RegularExpressions;
using ScraperLib;
using HtmlAgilityPack;

namespace ScraperLib
{
	public class BoerseStuttgart : WebScraper
	{
		public bool DummyMode { get; set; }

		public string Id { get; set; }
		public string Wkn { get; set; }
		public string Isin { get; set; }

		public BoerseStuttgart()
		{
			DummyMode = false;
		}

		public void FetchDataFromId (string id)
		{
			if (DummyMode) {
				Url = string.Format ("http://localhost/~johannes/boersenapp/{0}.html", id);
			} else {
				Url = string.Format ("https://www.boerse-stuttgart.de/rd/de/anlagezertifikate/factsheet?ID_NOTATION={0}.html", id);
			}
			ReadData();
			ParseData();
		}

		public override void ParseData ()
		{
			base.ParseData ();

			// Read ID from Chart URL
			HtmlNode id_node = Document.DocumentNode.SelectSingleNode ("//a[. = \"Chart\"]");
			string chart_url = id_node.Attributes["href"].Value;
			Match match = Regex.Match (chart_url, "ID_NOTATION=(\\d*)$");
			Id = match.Groups[1].Value;

			// Read information from tables
			Wkn = GetTableEntry("WKN");
			Isin = GetTableEntry ("ISIN");
		}

		protected string GetTableEntry (string key)
		{
			HtmlNode valueNode = Document.DocumentNode.SelectSingleNode (string.Format ("//td[. = \"{0}\"]/following-sibling::td[1]", key));
			return valueNode.InnerText;
		}
	}
}

