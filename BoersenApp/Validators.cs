using System;

namespace BoersenApp
{
	public class Validators
	{
		public static String NormalizeObject(object data) 
		{
			return data.ToString ();
		}

		public static bool ValidateDate (String input, ref object data)
		{
			DateTime dataAsDate;
			bool res = DateTime.TryParse (input, out dataAsDate);

			data = dataAsDate;
			return res;
		}

		public static String NormalizeDate(object data)
		{
			DateTime dataAsDate = (DateTime)data;

			return dataAsDate.ToString ("d");
		}

		public static bool ValidateInt (String input, ref object data)
		{
			int dataAsInt;

			bool res = Int32.TryParse (input, out dataAsInt);

			data=dataAsInt;
			return res;
		}

		public static bool ValidateDecimal (String input, ref object data)
		{
			decimal dataAsDecimal;

			bool res = Decimal.TryParse (input, out dataAsDecimal);

			data = dataAsDecimal;
			return res;
		}
	}
}

