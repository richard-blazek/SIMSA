using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class ColorConverterVM : ViewModelBase
	{
		static readonly ImmutableArray<IColorModel> models = ImmutableArray.Create<IColorModel>(new RGBColorModel(), new HTMLColorModel(), new CMYKColorModel(), new HSVColorModel(), new HSLColorModel());
		public ImmutableArray<RadioVM> Radios { get; }
		public ImmutableArray<EntryVM> Entries { get; private set; }
		int selected = 0;
		RGBColor color = new RGBColor(0, 0, 0);
		public Keyboard Keyboard => models[selected].NumbersOnly ? Keyboard.Numeric : Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
		public Color Color => color.ToXamarin;
		void InputChanged(int i)
		{
			var (texts, newColor) = models[selected].Parse(Entries.Select(s => s.Text).ToImmutableArray());
			color = newColor;
			Entries[i].Text = texts[i];
			PropertyChange("Color");
		}
		ImmutableArray<EntryVM> CreateInputs(IColorModel model, RGBColor color)
		{
			var values = model.ToStrings(color);
			return model.Names.Select((name, i) => new EntryVM(name, () => InputChanged(i), values[i])).ToImmutableArray();
		}
		void SelectModel(int newSelected)
		{
			selected = newSelected;
			Entries = CreateInputs(models[selected], color);
			PropertyChange("Entries");
		}
		public ColorConverterVM()
		{
			Radios = models.Select((m, i) => new RadioVM(m.Name, i == 0, () => SelectModel(i))).ToImmutableArray();
			Entries = CreateInputs(models[selected], color);
		}
	}
}
