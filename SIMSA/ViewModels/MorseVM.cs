using System.Windows.Input;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class MorseVM : ViewModelBase
	{
		MorseText text = new MorseText();
		public string AsBinary => text.Text.Replace('.', '0').Replace('-', '1').Replace('/', ',');
		public string Output => text.ToString();
		public string Input
		{
			get => text.Text;
			set => ChangePropertyUI(ref text, MorseText.Parse(value), text.Text, value, "Input", "Output");
		}
		public ICommand Invert { get; }
		void DoInvert() => ChangeProperty(ref text, text.Inverted, "Input", "Output");
		public MorseVM() => Invert = new Command(DoInvert);
	}
}
