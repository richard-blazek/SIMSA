using System;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageAlphabets : ContentPage
	{
		async void OpenEdit(Action<Config> save, int i)
		{
			var page = new EditAlphabet((BindingContext as ManageAlphabetsVM)!.Config, save, i);
			await Navigation.PushAsync(page, false);
		}

		public ManageAlphabets(Config config, Action<Config> save)
		{
			InitializeComponent();
			BindingContext = new ManageAlphabetsVM(config, save, OpenEdit);
		}
	}
}