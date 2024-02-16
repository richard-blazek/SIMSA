using System;
using System.Collections.Immutable;
using SIMSA.Models;

namespace SIMSA.ViewModels
{
	public class MenuVM : ViewModelConfiguratedBase
	{
		public ImmutableArray<ButtonVM> Buttons { get; }
		readonly Action<Config> save = x => { };
		public ImmutableArray<ViewModelBase> ViewModels { get; } = ImmutableArray<ViewModelBase>.Empty;
		public MenuVM(Config config, Action<Config> save, ImmutableArray<ButtonVM> buttons, ImmutableArray<ViewModelBase> viewModels)
			: base(config)
		{
			Buttons = buttons;
			ViewModels = viewModels;
			this.save = save;
		}
		public void ForEach<T>(Action<T> modifier)
		{
			foreach (var vm in ViewModels.OfType<T>())
			{
				modifier(vm);
			}
		}
		public override Config Config
		{
			get => base.Config;
			set
			{
				base.Config = value;
				ForEach<ViewModelConfiguratedBase>(vm => vm.Config = value);
				save(value);
			}
		}
	}
}
