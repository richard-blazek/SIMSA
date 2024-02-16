using System.ComponentModel;

namespace SIMSA.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void PropertyChange(params string[] names) => names.ForEach(name => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)));
		protected void ChangeProperty<T>(ref T variable, T newValue, params string[] effects)
		{
			if (!Equals(newValue, variable))
			{
				variable = newValue;
				PropertyChange(effects);
			}
		}
		protected void ChangePropertyUI<T, U>(ref T variable, T newValue, U oldDisplayed, U newDisplayed, params string[] effects)
		{
			if (!Equals(oldDisplayed, newDisplayed) || !Equals(newValue, variable))
			{
				variable = newValue;
				PropertyChange(effects);
			}
		}
	}
}
