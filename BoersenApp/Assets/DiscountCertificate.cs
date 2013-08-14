using System;

namespace BoersenApp.Assets
{
	public class Asset
	{
		public String Name { get; set; }
		public String Wkn { get; set; }
		public String Isin { get; set; }
		public virtual String Type { get; set; }
		public Decimal Rate { get; set; }
	}

	public class DiscountCertificate : Asset
	{
		public override string Type {
			get {
				return "Discountzertifikat";
			}
			set {
				throw new Exception("DiscountCertificate objects can't change type"); 
			}
		}

		public Decimal Cap { get; set; }
		public Decimal Ratio { get; set; }
		public DateTime Expiration { get; set; }

		public Decimal CalculatePayback (Decimal BaseRateAtExpiration)
		{
			if (BaseRateAtExpiration > Cap) {
				return Cap * Ratio;
			} else {
				return BaseRateAtExpiration * Ratio;
			}
		}
	}
}

