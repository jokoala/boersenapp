using System;
using NUnit.Framework;

using ScraperLib;

namespace UnitTests
{
	[TestFixture()]
	public class BoerseStuttgartTest
	{
		BoerseStuttgart boerseStuttgart;

		public BoerseStuttgartTest ()
		{
			boerseStuttgart = new BoerseStuttgart ();
			boerseStuttgart.DummyMode = true;
		}

		[Test()]
		public void FetchDataFromId_calledOnExampleAssets_returnsId ()
		{
			boerseStuttgart.FetchDataFromId ("75480544");

			Assert.AreEqual ("75480544", boerseStuttgart.Id);

			boerseStuttgart.FetchDataFromId ("70602330");
			Assert.AreEqual ("70602330", boerseStuttgart.Id);
		}

		[Test]
		public void FetchdataFromId_calledOnExampleAssets_returnsWkn ()
		{
			boerseStuttgart.FetchDataFromId ("75480544");
			Assert.AreEqual ("GT443X", boerseStuttgart.Wkn);

			boerseStuttgart.FetchDataFromId ("70602330");
			Assert.AreEqual ("GT3N7B", boerseStuttgart.Wkn);
		}

		[Test]
		public void FetchDataFromId_calledOnExampleAssets_returnsIsin ()
		{
			boerseStuttgart.FetchDataFromId ("75480544");
			Assert.AreEqual ("DE000GT443X6", boerseStuttgart.Isin);

			boerseStuttgart.FetchDataFromId ("70602330");
			Assert.AreEqual ("DE000GT3N7B6", boerseStuttgart.Isin);
		}

		[Test]
		public void FetchDataFromId_calledOnExampleAssets_returnsDiscountData ()
		{
			boerseStuttgart.FetchDataFromId("75480544");

			Assert.AreEqual(8000.00m, boerseStuttgart.Cap);
			Assert.AreEqual (0.01m, boerseStuttgart.Ratio);
			Assert.AreEqual (DateTime.Parse ("10.09.2014"), boerseStuttgart.Expiration);
		}

		[Test]
		public void FetchDataFromId_calledOnExampleAssets_returnsRate ()
		{
			boerseStuttgart.FetchDataFromId ("75480544");
			Assert.AreEqual (74.93m, boerseStuttgart.CurrentRate);

			boerseStuttgart.FetchDataFromId("70602330");
			Assert.AreEqual(67.45m, boerseStuttgart.CurrentRate);
		}

		[Test]
		public void FetchDataFromId_calledOnExampleAssets_setsBaseRate()
		{
			boerseStuttgart.FetchDataFromId("75480544");
			Assert.AreEqual(8368.99m, boerseStuttgart.BaseRate);

			boerseStuttgart.FetchDataFromId("70602330");
			Assert.AreEqual(8364.66m, boerseStuttgart.BaseRate);
		}

		[Test]
		public void FetchDataFromId_calledOnExampleAsset_setsName ()
		{
			boerseStuttgart.FetchDataFromId ("75480544");
			Assert.AreEqual ("Discount-Zertifikat auf DAX (Cap 8000)", boerseStuttgart.Name);

			boerseStuttgart.FetchDataFromId ("70602330");
			Assert.AreEqual ("Discount-Zertifikat auf DAX (Cap 7100)", boerseStuttgart.Name);
		}
	}
}

