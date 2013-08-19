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
	}
}

