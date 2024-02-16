using System;
using System.Linq;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class RadixConverterVM : ViewModelConfiguratedBase
	{
		public RadixConverterVM() : base(Config.Initial) { }
		int fromRadix, toRadix;
		string input = "";
		static string Filter(string s, long radix) => s.ToUpper().Where((c, i) => (i == 0 && c == '-') || (c >= '0' && c < '0' + radix) || (c >= 'A' && c < 'A' + radix - 10 && c <= 'Z')).Cat();
		public int FromRadix
		{
			get => fromRadix;
			set => ChangeProperty(ref fromRadix, value, "FromRadix", "Output", "InputKeyboard");
		}
		public int ToRadix
		{
			get => toRadix;
			set => ChangeProperty(ref toRadix, value, "ToRadix", "Output");
		}
		public string Input
		{
			get => input;
			set
			{
				ChangePropertyUI(ref input, Filter(value, fromRadix), input, value, "Input");
				ChangeProperty(ref fromRadix, Math.Max(2, fromRadix), "FromRadix");
				ChangeProperty(ref toRadix, Math.Max(2, toRadix), "ToRadix");
				PropertyChange("Output");
			}
		}
		public Keyboard InputKeyboard => fromRadix > 10 ? Keyboard.Create(KeyboardFlags.CapitalizeCharacter) : Keyboard.Numeric;
		public string Output => Filter(input, fromRadix).TryParse(fromRadix, out long value) ? value.ToString(toRadix) : "";
	}
}
