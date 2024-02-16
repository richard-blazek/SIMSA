using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new Pages.Menu(Config.Load(Properties, "Config"), SaveConfig));
		}

		async void SaveConfig(Config cfg)
		{
			cfg.Save(Properties, "Config");
			await SavePropertiesAsync();
		}

		protected override void OnStart() { }
		protected override void OnSleep() { }
		protected override void OnResume() { }
	}
}
