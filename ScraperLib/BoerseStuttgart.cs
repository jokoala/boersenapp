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
		public decimal CurrentRate { get; set; }
		public decimal Cap { get; set; }
		public decimal Ratio {get; set; }
		public DateTime Expiration { get; set; }
		public decimal BaseRate { get; set; }
		public string Name { get; set; }

		public BoerseStuttgart()
		{
			DummyMode = false;
		}

		public void FetchDataFromId (string id)
		{
			if (DummyMode) {
				Url = string.Format ("http://localhost/~johannes/boersenapp/{0}.html", id);
			} else {
				Url = string.Format ("https://www.boerse-stuttgart.de/de/factsheet/anlagezertifikate/uebersicht.html?&ID_NOTATION={0}",id);
			}
			ReadData();
			ParseData();
		}

		public void FetchDataFromSearch (string search)
		{
			if (DummyMode) {
				Url = string.Format ("http://localhost/~johannes/boersenapp/{0}.html", search);
			} else {
				Url = string.Format ("https://www.boerse-stuttgart.de/rd/de/search/?searchterm={0}&submitheadsearch=Suchen", search);
			}
			ReadData ();
			ParseData ();
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
			Name = GetTableEntry("Produktname");

			// Read Cap
			match = Regex.Match (GetTableEntry ("Cap").Trim (), "^([0-9.,]*) .*$");
			Cap = decimal.Parse (match.Groups[1].Value);

			// Read Ratio
			match = Regex.Match (GetTableEntry ("Bezugsver").Trim (), "^(\\d*)\\s*:\\s*(\\d*)$");
			Ratio = decimal.Parse (match.Groups[2].Value) / decimal.Parse (match.Groups[1].Value);

			// Read Expiration Date
			Expiration = DateTime.Parse (GetTableEntry ("Letzter Bewert"));

			// Read Rate
			match = Regex.Match (GetTableEntry ("Last").Trim (), "^(\\d*,\\d*)");
			CurrentRate = decimal.Parse (match.Groups[1].Value);

			// Read Base Value
			match = Regex.Match (GetTableEntry("Basiswert").Trim(), "\\(([0-9.,]*)\\)$");
			BaseRate = decimal.Parse (match.Groups[1].Value);
		}

		protected string GetTableEntry (string key)
		{
			HtmlNode valueNode = Document.DocumentNode.SelectSingleNode (string.Format ("//td[. = \"{0}\"]/following-sibling::td[1]", key));
			if (valueNode == null) {
				valueNode = Document.DocumentNode.SelectSingleNode (string.Format ("//td[contains(.//span, \"{0}\")]/following-sibling::td[1]", key));
			}
			return valueNode.InnerText;
		}
	}
}

