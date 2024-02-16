using System;
using SIMSA.Models;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectAlphabet : ContentPage
	{
		public SelectAlphabet(Config config, Action<IAlphabet> onSelected)
		{
			InitializeComponent();
			BindingContext = new SelectAlphabetVM(config, async alphabet =>
			{
				onSelected(alphabet);
				_ = await Navigation.PopAsync(false);
			});
		}
	}
}