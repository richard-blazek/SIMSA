using System.Collections.Immutable;
using System.Windows.Input;
using SIMSA.Models;
using SIMSA.Resources;
using Xamarin.Forms;
using Language = SIMSA.Models.FrequencyAnalyser.Language;

namespace SIMSA.ViewModels
{
	public class FrequencyAnalysisVM : ViewModelBase
	{
		string input = "";
		Language language = Language.English;
		ImmutableArray<LetterFrequency> analysisOutput = ImmutableArray<LetterFrequency>.Empty;
		public ImmutableArray<LetterFrequency> AnalysisOutput => analysisOutput;
		public ImmutableArray<LetterFrequency> LanguageOutput => FrequencyAnalyser.Statistics[language];
		public string LanguageName => language switch { Language.English => AppResources.English, Language.CzechDiacritics => AppResources.CzechDiacritics, _ => AppResources.Czech };
		public string Input
		{
			get => input;
			set => ChangeProperty(ref input, value, "Input");
		}
		public ICommand Analyse { get; }
		public ICommand SwitchLanguage { get; }
		public FrequencyAnalysisVM()
		{
			Analyse = new Command(() => ChangeProperty(ref analysisOutput, FrequencyAnalyser.Analyse(input), "AnalysisOutput"));
			SwitchLanguage = new Command(() => ChangeProperty(ref language, FrequencyAnalyser.Next(language), "LanguageOutput", "LanguageName"));
		}
	}
}
