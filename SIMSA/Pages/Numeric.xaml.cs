using System;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Numeric : ContentPage
	{
		Config Config => (BindingContext as NumericVM)!.Config;
		async void SelectAlphabet(Action<IAlphabet> then) => await Navigation.PushAsync(new SelectAlphabet(Config, then), false);
		public Numeric(Config config)
		{
			InitializeComponent();
			BindingContext = new NumericVM(config, SelectAlphabet);
		}
	}
}