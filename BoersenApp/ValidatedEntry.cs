using System;
using Gtk;

namespace BoersenApp
{
	[System.ComponentModel.ToolboxItem(true)]
	public class ValidatedEntry : Gtk.Entry
	{
		Gdk.Color invalidColor;
		object parsedText;

		public delegate bool Validator(String input, ref object res);
		public delegate String Normalizer(object data);

		public virtual Validator Validate { get; set; }
		public virtual Normalizer Normalize { get; set; }

		public virtual Gdk.Color InvalidColor {
			get {
				return invalidColor;
			}
			set {
				invalidColor = value;
			}
		}

		public object ParsedText {
			get {
				if (Validate == null)
					return null;
				if (Validate (this.Text, ref parsedText)) {
					return parsedText;
				} else {
					return null;
				}
			}

			set {
				parsedText = value;
				if (Normalize == null) {
					Text = parsedText.ToString();
				} else {
					Text = Normalize(parsedText);
				}
			}
		}

		public ValidatedEntry ()
		{
			Gdk.Color.Parse ("yellow", ref invalidColor);
			this.FocusOutEvent += new FocusOutEventHandler(ValidateOnFocusOutEvent);
			this.FocusInEvent += new FocusInEventHandler(ValidateOnFocusInEvent);
		}

		protected void ValidateOnFocusOutEvent (object o, FocusOutEventArgs args)
		{
			if (Validate == null)
				return;
			if (!Validate (this.Text, ref parsedText)) {
				this.ModifyBase (StateType.Normal, invalidColor);
			} else {
				if (Normalize != null) {
					this.Text = Normalize (parsedText);
				}
			}
		}	

		protected void ValidateOnFocusInEvent (object o, FocusInEventArgs args)
		{
			this.ModifyBase (StateType.Normal);
		}
	}
}

