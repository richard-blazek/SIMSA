using System;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Input;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
		readonly ImmutableArray<ContentPage> pages;
		void Save(Config config) => (BindingContext as MenuVM)!.Config = config;
		void ForEachViewModel<T>(Action<T> modifier) => (BindingContext as MenuVM)!.ForEach(modifier);
		void ForEachPage<T>(Action<T> modifier) => pages.ForEach(modifier);
		ICommand OpenCommand(ContentPage page) => new Command(async () => await Navigation.PushAsync(page, false));
		public Menu(Config config, Action<Config> save)
		{
			InitializeComponent();
			pages = new ContentPage[]
			{
				new Braille(),
				new ColorConverter(),
				new FlagSemaphore(),
				new FrequencyAnalysis(),
				new Morse(ForEachViewModel, ForEachPage),
				new Numeric(config),
				new Playfair(),
				new Primes(),
				new RadixConverter(),
				new Vigenere(config),
				new ManageAlphabets(config, Save)
			}.ToImmutableArray();
			var buttons = pages.Select(page => new ButtonVM(page.Title, OpenCommand(page))).ToImmutableArray();
			var viewModels = pages.Select(page => page.BindingContext).OfType<ViewModelBase>().ToImmutableArray();
			BindingContext = new MenuVM(config, save, buttons, viewModels);
		}
	}
}