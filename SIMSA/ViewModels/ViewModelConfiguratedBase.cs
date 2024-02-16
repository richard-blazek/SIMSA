using SIMSA.Models;

namespace SIMSA.ViewModels
{
	public abstract class ViewModelConfiguratedBase : ViewModelBase
	{
		public virtual Config Config { get; set; }
		protected ViewModelConfiguratedBase(Config config) => Config = config;
	}
}
