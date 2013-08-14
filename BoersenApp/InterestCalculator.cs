using System;

namespace BoersenApp
{
	public class InterestCalculator
	{
		public InterestCalculator ()
		{
		}

		public static double GetInterest(decimal redemption, int timespan)
		{
			double redemptionPerYear = Math.Exp ((Math.Log ((double)redemption) / timespan )*365);
			return (redemptionPerYear - 1)*100;
		}

		public static double GetInterest (decimal redemption, DateTime start, DateTime end)
		{
			int timespan = (int)(end - start).TotalDays;
			return GetInterest (redemption, timespan);
		}
	}
}

