using System;
using BoersenApp;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class InterestCalculatorTest
	{
		static double tolerance = 0.000001;

		[Test]
		public void GetInterest_Period365Days_returnsCorrectInterest()
		{
			double res = InterestCalculator.GetInterest (1.10m, 365);
			Assert.AreEqual ( 10.0, res, tolerance);

			res = InterestCalculator.GetInterest(1.05m, 365);
			Assert.AreEqual ( 5.0, res, tolerance);
		}

		[Test]
		public void GetInterest_PeriodYearGivenByDate_returnsCorrectInterest ()
		{
			DateTime start = DateTime.Parse ("2013-01-01");
			DateTime end = DateTime.Parse ("2014-01-01");

			double res = InterestCalculator.GetInterest (1.10m, start, end);
			Assert.AreEqual (10.0, res, tolerance);

			res = InterestCalculator.GetInterest(1.05m, start, end);
			Assert.AreEqual(5.0, res, tolerance);
		}

		[Test]
		public void GetInterest_PeriodArbitrary_returnsCorrectInterest()
		{
			double res = InterestCalculator.GetInterest(1.10m, 182);
			Assert.AreEqual (21.06338215, res, tolerance);

			res = InterestCalculator.GetInterest(1.21m, 365*2);
			Assert.AreEqual (10.0, res, tolerance);
		}
	}
}

