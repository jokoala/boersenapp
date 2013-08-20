using System;
using Gtk;
using BoersenApp;
using Assets = BoersenApp.Assets;
using ScraperLib;

// TODO: Pop-up for date dialog
// TODO: input validation
public partial class MainWindow: Gtk.Window {	
	uint m_context_id;
	Assets.DiscountCertificate asset;
	BoerseStuttgart boerseStuttgart;

	/// <summary>
	/// Gets the asset.
	/// </summary>
	/// <value>
	/// The asset.
	/// </value>
	public Assets.DiscountCertificate Asset {
		get {
			try {
				FillAsset ();
				return asset;
			} catch (NullReferenceException) {
				return null;
			}
		}
	}

	protected void FillAsset ()
	{
		asset.Name = std_wertpapier.ActiveText;
		asset.Wkn = std_wkn.Text;
		asset.Isin = std_isin.Text;
		asset.Expiration = (DateTime)discount_lfz.ParsedText;
		asset.Cap = (decimal)discount_cap.ParsedText;
		asset.Ratio = (decimal)discount_bezug.ParsedText;

		asset.Rate = (decimal)szenario_kauf.ParsedText;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MainWindow"/> class.
	/// </summary>
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		asset = new Assets.DiscountCertificate();
		boerseStuttgart = new BoerseStuttgart();
	
		m_context_id= statusbar1.GetContextId("MainWindow");

		discount_lfz.Validate = Validators.ValidateDate;
		discount_lfz.Normalize = Validators.NormalizeDate;
		discount_cap.Validate = Validators.ValidateDecimal;
		discount_bezug.Validate = Validators.ValidateDecimal;

		szenario_kauf.Validate = Validators.ValidateDecimal;
		szenario_base.Validate = Validators.ValidateDecimal;
		szenario_end.Validate = Validators.ValidateDecimal;
	}

	protected void notify (String message)
	{
		statusbar1.Push (m_context_id, message);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnComboboxentry1Changed (object sender, EventArgs e)
	{
		notify(String.Format ("Entry changed to: {0}", std_wertpapier.Active));
	}	

	protected void OnQuitActionActivated (object sender, EventArgs e)
	{
		Application.Quit ();
	}	

	protected void OnStdTypChanged (object sender, EventArgs e)
	{
		typ.Page = std_typ.Active;
	}

	protected void updateResult ()
	{
		try {
			decimal payback = Asset.CalculatePayback ((decimal)szenario_end.ParsedText);
			decimal interest = decimal.Round ((payback/asset.Rate - 1)*100,4);
			double interestPA = InterestCalculator.GetInterest(payback/asset.Rate, DateTime.Now, (DateTime)discount_lfz.ParsedText);

			string color;
			if (payback >= asset.Rate) {
				color = "green";
			} else {
				color = "red";
			}
			PayBack.LabelProp = String.Format ("<span color=\"{1}\" size=\"x-large\">{0:0.00}</span>", payback, color);
			Interest.LabelProp = String.Format ("<span color=\"{1}\" size=\"x-large\">{0:0.00} %</span>", interest, color);
			InterestPA.LabelProp = String.Format ("<span color=\"{1}\" size=\"x-large\">{0:0.00} %</span>", interestPA, color);
		} catch (NullReferenceException) {
		}
	}	

	protected void OnSzenarioEndChanged (object sender, EventArgs e)
	{
		updateResult();
	}	

	protected void OnSzenarioEndscaleChangeValue (object o, ChangeValueArgs args)
	{
		decimal baseVal = (decimal)szenario_base.ParsedText;
		decimal endVal = decimal.Round (baseVal * (decimal)szenario_endscale.Value/100, 4);
		szenario_end.Text = endVal.ToString ();
	}	

	protected void LoadFromStuttgart ()
	{
		std_wertpapier.PrependText(boerseStuttgart.Name);
		std_wertpapier.Active=0;
		std_wkn.Text = boerseStuttgart.Wkn;
		std_isin.Text = boerseStuttgart.Isin;

		discount_lfz.ParsedText = boerseStuttgart.Expiration;
		discount_cap.ParsedText = boerseStuttgart.Cap;
		discount_bezug.ParsedText = boerseStuttgart.Ratio;

		szenario_kauf.ParsedText = boerseStuttgart.CurrentRate;
		szenario_base.ParsedText = boerseStuttgart.BaseRate;

		OnSzenarioEndscaleChangeValue(null, null);
	}

	protected void OnLoadButtonClicked (object sender, EventArgs e)
	{
		Console.WriteLine(std_wertpapier.ActiveText);
		boerseStuttgart.FetchDataFromSearch(std_wertpapier.ActiveText);
		LoadFromStuttgart();
	}





}
