using System;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAlphabet : ContentPage
	{
		public EditAlphabet(Config config, Action<Config> save, int i)
		{
			InitializeComponent();
			BindingContext = new EditAlphabetVM(config, save, i);
		}

		void Close(object sender, EventArgs e) => Navigation.PopAsync(false);
	}
}