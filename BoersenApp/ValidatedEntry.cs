using System;
using Gtk;

namespace BoersenApp
{
	[System.ComponentModel.ToolboxItem(true)]
	public class ValidatedEntry : Gtk.Entry
	{
		public delegate bool Validator(String input);

		public Validator validate { get; set; }
		public Gdk.Color invalid_color { get; set; }

		public ValidatedEntry ()
		{
			Gdk.Color tmp_invalid_color;
			Gdk.Color.Parse ("yellow", ref tmp_invalid_color);
			invalid_color = tmp_invalid_color;

			this.FocusOutEvent += new FocusOutEventHandler(ValidateOnFocusOutEvent);
			this.FocusInEvent += new FocusInEventHandler(ValidateOnFocusInEvent);
		}

		protected void ValidateOnFocusOutEvent (object o, FocusOutEventArgs args)
		{
			if (validate == null) return;
			if (!validate (this.Text)) 
			{
				this.ModifyBase (StateType.Normal, invalid_color);
			}
		}	

		protected void ValidateOnFocusInEvent (object o, FocusInEventArgs args)
		{
			this.ModifyBase (StateType.Normal);
		}

	}
}

