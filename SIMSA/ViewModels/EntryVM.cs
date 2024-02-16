using System;

namespace SIMSA.ViewModels
{
	public class EntryVM : ViewModelBase
	{
		string text;
		readonly Action changed;
		public string Caption { get; }
		public EntryVM(string caption, Action changed, string text)
		{
			Caption = caption;
			this.changed = changed;
			this.text = text;
		}
		public string Text
		{
			get => text;
			set
			{
				if (text != value)
				{
					text = value;
					changed();
					PropertyChange("Text");
				}
			}
		}
	}
}
