using SIMSA.Models;

namespace SIMSA.ViewModels
{
	public class PlayfairVM : ViewModelBase
	{
		PlayfairText text = new PlayfairText();
		public string Output => text.ToString();
		public string Input
		{
			get => text.Text;
			set => ChangeProperty(ref text, text.WithText(value), "Input", "Output");
		}
		public string Key
		{
			get => text.Key;
			set => ChangeProperty(ref text, text.WithKey(value), "Key", "Output");
		}
		public char Replaced
		{
			get => text.Replaced;
			set => ChangeProperty(ref text, text.WithReplaced(value), "Replace", "Output");
		}
		public PlayfairVM() { }
	}
}
