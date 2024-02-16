using System;
using System.Windows.Input;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class NumericVM : ViewModelConfiguratedBase
	{
		NumericText text;
		public string Output => text.ToString();
		public string Input
		{
			get => text.Text;
			set => ChangePropertyUI(ref text, text.WithText(value), text.Text, value, "Input", "Output");
		}
		public byte Radix
		{
			get => text.Radix;
			set => ChangePropertyUI(ref text, text.WithRadix(value), text.Radix, value, "Input", "Output", "Radix", "InputKeyboard");
		}
		public string AlphabetName => text.Alphabet.Name;
		public ICommand Invert { get; }
		public ICommand SelectAlphabet { get; }
		void SetAlphabet(IAlphabet alphabet) => ChangeProperty(ref text, text.WithAlphabet(alphabet), "Output");
		public NumericVM(Config config, Action<Action<IAlphabet>> selectAlphabet) : base(config)
		{
			text = new NumericText(config.DefaultAlphabet);
			Invert = new Command(() => ChangeProperty(ref text, text.Invert(), "Input", "Output"));
			SelectAlphabet = new Command(() =>
			{
				selectAlphabet(SetAlphabet);
				PropertyChange("AlphabetName");
			});
		}
	}
}
