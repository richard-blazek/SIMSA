using System;
using SIMSA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Morse : ContentPage
	{
		readonly Action<Action<NumericVM>> forEachViewModel;
		readonly Action<Action<Numeric>> forEachPage;
		void ChangeToBinary(NumericVM vm)
		{
			vm.Radix = 2;
			vm.Input = (BindingContext as MorseVM)!.AsBinary;
		}
		async void GoToBinary(Numeric page)
		{
			Navigation.InsertPageBefore(page, this);
			_ = await Navigation.PopAsync();
		}
		void ToBinary(object sender, EventArgs e)
		{
			forEachViewModel(ChangeToBinary);
			forEachPage(GoToBinary);
		}
		public Morse(Action<Action<NumericVM>> forEachViewModel, Action<Action<Numeric>> forEachPage)
		{
			this.forEachViewModel = forEachViewModel;
			this.forEachPage = forEachPage;
			InitializeComponent();
		}
	}
}