using System.Windows.Input;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class BrailleVM : ViewModelConfiguratedBase
	{
		BrailleText text = new BrailleText();
		public ICommand Backspace { get; }
		public ICommand Clear { get; }
		public ICommand Confirm { get; }
		public ICommand Invert { get; }
		public ICommand InvertAt { get; }
		public ICommand Mirror { get; }
		public ICommand Turn { get; }
		public BrailleVM() : base(Config.Initial)
		{
			Backspace = new Command(() => ChangeProperty(ref text, text.Pop(), "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));
			Clear = new Command(() => ChangeProperty(ref text, new BrailleText(), "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));
			Confirm = new Command(() => ChangeProperty(ref text, text.Add(0), "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));
			Invert = new Command(() => ChangeProperty(ref text, text.Inverted, "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));
			Mirror = new Command(() => ChangeProperty(ref text, text.Mirrored, "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));
			Turn = new Command(() => ChangeProperty(ref text, text.Turned, "ColorAt0", "ColorAt1", "ColorAt2", "ColorAt3", "ColorAt4", "ColorAt5", "Output"));

			InvertAt = new Command<string>(index =>
			{
				if (int.TryParse(index, out int i))
				{
					ChangeProperty(ref text, text.InvertAt(i), "ColorAt" + i, "Output");
				}
			});
		}
		static Color ColorOf(bool bit) => bit ? new Color(1, 1, 1) : new Color(0, 0, 0);
		public Color ColorAt0 => ColorOf(text[^1, 0]);
		public Color ColorAt1 => ColorOf(text[^1, 1]);
		public Color ColorAt2 => ColorOf(text[^1, 2]);
		public Color ColorAt3 => ColorOf(text[^1, 3]);
		public Color ColorAt4 => ColorOf(text[^1, 4]);
		public Color ColorAt5 => ColorOf(text[^1, 5]);
		public string Output => text.ToString();
	}
}
