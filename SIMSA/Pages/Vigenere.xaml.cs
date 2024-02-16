using System;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Vigenere : ContentPage
	{
		Config Config => (BindingContext as VigenereVM)!.Config;
		async void SelectAlphabet(Action<IAlphabet> then) => await Navigation.PushAsync(new SelectAlphabet(Config, then), false);
		public Vigenere(Config config)
		{
			InitializeComponent();
			BindingContext = new VigenereVM(config, SelectAlphabet);
		}
	}
}