using System;
using System.Collections.Immutable;
using System.Linq;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class SelectAlphabetVM : ViewModelConfiguratedBase
	{
		readonly Action<IAlphabet> select;
		ButtonVM ButtonFor(IAlphabet alphabet) => new ButtonVM(alphabet.Name, new Command(() => select(alphabet)));
		public ImmutableArray<ButtonVM> Buttons => Config.Alphabets.Select(ButtonFor).ToImmutableArray();
		public override Config Config
		{
			get => base.Config;
			set
			{
				base.Config = value;
				PropertyChange("Buttons");
			}
		}
		public SelectAlphabetVM(Config config, Action<IAlphabet> select) : base(config) => this.select = select;
	}
}
