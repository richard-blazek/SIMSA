using System;
using System.Collections.Immutable;
using System.Linq;
using SIMSA.Models;
using SIMSA.Resources;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class ManageAlphabetsVM : ViewModelConfiguratedBase
	{
		readonly Action<Action<Config>, int> select;
		readonly Action<Config> save;
		ButtonVM ButtonFor(string name, int i) => new ButtonVM(name, new Command(() => select(save, i)));
		void AddAlphabet()
		{
			save(Config.Add(CustomAlphabet.Empty));
			select(save, Config.Alphabets.Custom.Count - 1);
		}
		ButtonVM AlphabetCreator => new ButtonVM(AppResources.AddAlphabet, new Command(AddAlphabet));
		public ImmutableArray<ButtonVM> Buttons => Config.Alphabets.Custom.Select((abc, i) => ButtonFor(abc.Name, i)).Append(AlphabetCreator).ToImmutableArray();
		public override Config Config
		{
			get => base.Config;
			set
			{
				base.Config = value;
				PropertyChange("Buttons");
			}
		}
		public ManageAlphabetsVM(Config config, Action<Config> save, Action<Action<Config>, int> select) : base(config)
		{
			this.select = select;
			this.save = save;
		}
	}
}
