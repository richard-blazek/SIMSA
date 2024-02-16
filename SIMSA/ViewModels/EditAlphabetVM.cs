using System;
using System.Windows.Input;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class EditAlphabetVM : ViewModelConfiguratedBase
	{
		readonly Action<Config> save;
		readonly int alphabetIndex;
		CustomAlphabet alphabet;
		public ICommand Save { get; }
		public ICommand Delete { get; }
		public string Name
		{
			get => alphabet.Name;
			set => ChangeProperty(ref alphabet, alphabet.WithName(value), "Name");
		}
		public string Letters
		{
			get => alphabet.ToString();
			set => ChangePropertyUI(ref alphabet, alphabet.WithLetters(value), alphabet.ToString(), value, "Letters");
		}

		void DoSave() => save(Config.With(Config.Alphabets.Update(alphabetIndex, alphabet)));
		void DoDelete() => save(Config.With(Config.Alphabets.Remove(alphabetIndex)));
		public EditAlphabetVM(Config config, Action<Config> save, int alphabetIndex) : base(config)
		{
			this.save = save;
			this.alphabetIndex = alphabetIndex;

			alphabet = config.Alphabets.Custom[alphabetIndex];
			Save = new Command(DoSave);
			Delete = new Command(DoDelete);
		}
	}
}
