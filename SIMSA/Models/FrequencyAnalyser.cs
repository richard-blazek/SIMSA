using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSA.Models
{
	public static class FrequencyAnalyser
	{
		public enum Language { English, CzechDiacritics, Czech }
		public static Language Next(Language language) => (Language)(((int)language + 1) % 3);
		public static readonly ImmutableDictionary<Language, ImmutableArray<LetterFrequency>> Statistics = new Dictionary<Language, ImmutableArray<LetterFrequency>>
		{
			{
				Language.English, new[]
				{
					new LetterFrequency('A', 0.08167),
					new LetterFrequency('B', 0.01492),
					new LetterFrequency('C', 0.02782),
					new LetterFrequency('D', 0.04253),
					new LetterFrequency('E', 0.12702),
					new LetterFrequency('F', 0.02228),
					new LetterFrequency('G', 0.02015),
					new LetterFrequency('H', 0.06094),
					new LetterFrequency('I', 0.06966),
					new LetterFrequency('J', 0.00153),
					new LetterFrequency('K', 0.00772),
					new LetterFrequency('L', 0.04025),
					new LetterFrequency('M', 0.02406),
					new LetterFrequency('N', 0.06749),
					new LetterFrequency('O', 0.07507),
					new LetterFrequency('P', 0.01929),
					new LetterFrequency('Q', 0.00095),
					new LetterFrequency('R', 0.05987),
					new LetterFrequency('S', 0.06327),
					new LetterFrequency('T', 0.09056),
					new LetterFrequency('U', 0.02758),
					new LetterFrequency('V', 0.00978),
					new LetterFrequency('W', 0.02360),
					new LetterFrequency('X', 0.00150),
					new LetterFrequency('Y', 0.01974),
					new LetterFrequency('Z', 0.00074)
				}.OrderByDescending(p => p.Frequency).ToImmutableArray()
			},
			{
				Language.CzechDiacritics, new[]
				{
					new LetterFrequency('A', 0.06147321),
					new LetterFrequency('Á', 0.022096275),
					new LetterFrequency('B', 0.015401662),
					new LetterFrequency('C', 0.027454535),
					new LetterFrequency('Č', 0.009380168),
					new LetterFrequency('D', 0.035602135),
					new LetterFrequency('Ď', 0.000219431),
					new LetterFrequency('E', 0.076061397),
					new LetterFrequency('É', 0.01319154),
					new LetterFrequency('Ě', 0.016262581),
					new LetterFrequency('F', 0.002700381),
					new LetterFrequency('G', 0.002697416),
					new LetterFrequency('H', 0.024138364),
					new LetterFrequency('I', 0.043024229),
					new LetterFrequency('Í', 0.032320559),
					new LetterFrequency('J', 0.020948712),
					new LetterFrequency('K', 0.036934534),
					new LetterFrequency('L', 0.0379793),
					new LetterFrequency('M', 0.031893558),
					new LetterFrequency('N', 0.064596638),
					new LetterFrequency('Ň', 0.000804579),
					new LetterFrequency('O', 0.085660995),
					new LetterFrequency('Ó', 0.000309377),
					new LetterFrequency('P', 0.033732032),
					new LetterFrequency('Q', 0.0000128495),
					new LetterFrequency('R', 0.036542128),
					new LetterFrequency('Ř', 0.012025197),
					new LetterFrequency('S', 0.044637341),
					new LetterFrequency('Š', 0.00795881),
					new LetterFrequency('T', 0.05660521),
					new LetterFrequency('Ť', 0.00042107),
					new LetterFrequency('U', 0.031079095),
					new LetterFrequency('Ú', 0.001019068),
					new LetterFrequency('Ů', 0.006867587),
					new LetterFrequency('V', 0.04607649),
					new LetterFrequency('W', 0.0000869815),
					new LetterFrequency('X', 0.000746262),
					new LetterFrequency('Y', 0.018872027),
					new LetterFrequency('Ý', 0.010596921),
					new LetterFrequency('Z', 0.021732534),
					new LetterFrequency('Ž', 0.009836821)
				}.OrderByDescending(p => p.Frequency).ToImmutableArray()
			},
			{
				Language.Czech, new[]
				{
					new LetterFrequency('A', 0.084548),
					new LetterFrequency('B', 0.015582),
					new LetterFrequency('C', 0.031411),
					new LetterFrequency('D', 0.036241),
					new LetterFrequency('E', 0.106751),
					new LetterFrequency('F', 0.002732),
					new LetterFrequency('G', 0.002729),
					new LetterFrequency('H', 0.018566),
					new LetterFrequency('I', 0.076227),
					new LetterFrequency('J', 0.021194),
					new LetterFrequency('K', 0.037367),
					new LetterFrequency('L', 0.038424),
					new LetterFrequency('M', 0.032267),
					new LetterFrequency('N', 0.066167),
					new LetterFrequency('O', 0.086977),
					new LetterFrequency('P', 0.034127),
					new LetterFrequency('Q', 0.000013),
					new LetterFrequency('R', 0.049136),
					new LetterFrequency('S', 0.053212),
					new LetterFrequency('T', 0.057694),
					new LetterFrequency('U', 0.039422),
					new LetterFrequency('V', 0.046616),
					new LetterFrequency('W', 0.000088),
					new LetterFrequency('X', 0.000755),
					new LetterFrequency('Y', 0.029814),
					new LetterFrequency('Z', 0.031939)
				}.OrderByDescending(p => p.Frequency).ToImmutableArray()
			}
		}.ToImmutableDictionary();
		public static ImmutableArray<LetterFrequency> Analyse(string text)
		{
			var occurences = new Dictionary<char, int>();
			for (int i = 0; i < text.Length; ++i)
			{
				if (!char.IsWhiteSpace(text[i]) && !char.IsControl(text[i]))
				{
					occurences[text[i]] = occurences.GetValueOrDefault(text[i], 0) + 1;
				}
			}
			var sum = (double)occurences.Sum(pair => pair.Value);
			return occurences.Select(p => new LetterFrequency(p.Key, p.Value / sum)).OrderByDescending(p => p.Frequency).ToImmutableArray();
		}
	}
}
