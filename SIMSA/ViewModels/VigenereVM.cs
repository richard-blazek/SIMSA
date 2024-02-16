using System;
using System.Windows.Input;
using SIMSA.Models;
using SIMSA.Resources;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class VigenereVM : ViewModelConfiguratedBase
	{
		VigenereText text;
		public string Output => text.ToString();
		public string Input
		{
			get => text.Text;
			set => ChangeProperty(ref text, text.WithText(value), "Input", "Output");
		}
		public string Key
		{
			get => text.Text;
			set => ChangeProperty(ref text, text.WithKey(value), "Key", "Output");
		}
		public string Sign => text.Minus ? AppResources.Subtract : AppResources.Add;
		public string AlphabetName => text.Alphabet.Name;
		public ICommand InvertSign { get; }
		public ICommand SelectAlphabet { get; }
		void SetAlphabet(IAlphabet alphabet) => ChangeProperty(ref text, text.WithAlphabet(alphabet), "Output", "AlphabetName");
		public VigenereVM(Config config, Action<Action<IAlphabet>> selectAlphabet) : base(config)
		{
			text = new VigenereText(config.DefaultAlphabet);
			InvertSign = new Command(() => ChangeProperty(ref text, text.InvertSign(), "Input", "Output", "Sign"));
			SelectAlphabet = new Command(() => selectAlphabet(SetAlphabet));
		}
	}
}
