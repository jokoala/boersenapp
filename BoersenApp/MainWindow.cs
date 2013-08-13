using System;
using Gtk;


// TODO: Pop-up for date dialog
// TODO: input validation
public partial class MainWindow: Gtk.Window {	
	uint m_context_id;
	Gdk.Color m_invalid_color;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		m_context_id= statusbar1.GetContextId("MainWindow");
		Gdk.Color.Parse ("yellow", ref m_invalid_color);
		std_wkn.validate = ValidateDate;
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

	protected bool ValidateDate (String input)
	{
		if (input == "") {
			return true;
		}

		DateTime result;
		return (DateTime.TryParse (input, out result));
	}

	protected void OnBonusLfzFocusOutEvent (object o, FocusOutEventArgs args)
	{
		if (!ValidateDate (bonus_lfz.Text)) 
		{
			bonus_lfz.ModifyBase (StateType.Normal, m_invalid_color);
		}
	}	

	protected void OnBonusLfzFocusInEvent (object o, FocusInEventArgs args)
	{
		bonus_lfz.ModifyBase (StateType.Normal);
	}

}
