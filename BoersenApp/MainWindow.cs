using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	uint m_context_id;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		m_context_id= statusbar1.GetContextId("MainWindow");
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
