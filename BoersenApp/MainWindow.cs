using System;
using Gtk;
using BoersenApp;

// TODO: Pop-up for date dialog
// TODO: input validation
public partial class MainWindow: Gtk.Window {	
	uint m_context_id;


	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		m_context_id= statusbar1.GetContextId("MainWindow");

		discount_lfz.Validate = Validators.ValidateDate;
		discount_lfz.Normalize = Validators.NormalizeDate;
		discount_cap.Validate = Validators.ValidateDecimal;

		szenario_kauf.Validate = Validators.ValidateDecimal;
		szenario_base.Validate = Validators.ValidateDecimal;
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



}
