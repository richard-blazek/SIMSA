using System.Windows.Input;

namespace SIMSA.ViewModels
{
	public class ButtonVM
	{
		public string Title { get; }
		public ICommand Command { get; }
		public ButtonVM(string title, ICommand command) => (Title, Command) = (title, command);
	}
}
