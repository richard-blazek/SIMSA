using System;

namespace SIMSA.ViewModels
{
	public class RadioVM : ViewModelBase
	{
		bool isChecked;
		readonly Action onChecked;
		public string Text { get; }
		public RadioVM(string text, bool isChecked, Action onChecked)
		{
			Text = text;
			this.isChecked = isChecked;
			this.onChecked = onChecked;
		}
		public bool IsChecked
		{
			get => isChecked;
			set
			{
				if (value != isChecked)
				{
					isChecked = value;
					if (value)
					{
						onChecked();
					}
					PropertyChange("IsChecked");
				}
			}
		}
	}
}
