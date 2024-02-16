using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSA.Models
{
	public class MorseText
	{
		static readonly ImmutableDictionary<char, string> LetterToMorse = new Dictionary<char, string>
		{
			{'A', ".-"},
			{'B', "-..."},
			{'C', "-.-."},
			{'D', "-.."},
			{'E', "."},
			{'F', "..-."},
			{'G', "--."},
			{'H', "...."},
			{'I', ".."},
			{'J', ".---"},
			{'K', "-.-"},
			{'L', ".-.."},
			{'M', "--"},
			{'N', "-."},
			{'O', "---"},
			{'P', ".--."},
			{'Q', "--.-"},
			{'R', ".-."},
			{'S', "..."},
			{'T', "-"},
			{'U', "..-"},
			{'V', "...-"},
			{'W', ".--"},
			{'X', "-..-"},
			{'Y', "-.--"},
			{'Z', "--.."},
			{'0', "-----"},
			{'1', ".----"},
			{'2', "..---"},
			{'3', "...--"},
			{'4', "....-"},
			{'5', "....."},
			{'6', "-...."},
			{'7', "--..."},
			{'8', "---.."},
			{'9', "----."},
			{'.', ".-.-.-"},
			{',', "--..--"},
			{'?', "..--.."},
			{'\'', ".----."},
			{'/', "-..-."},
			{'(', "-.--."},
			{')', "-.--.-"},
			{':', "---..."},
			{'=', "-...-"},
			{'+', ".-.-."},
			{'-', "-....-"},
			{'"', ".-..-."}
		}.ToImmutableDictionary();
		static readonly ImmutableDictionary<string, char> MorseToLetter = LetterToMorse.ToImmutableDictionary(kv => kv.Value, kv => kv.Key);

		public string Text { get; }
		public MorseText() : this(string.Empty) { }

		MorseText(string text) => Text = text;
		public static MorseText Parse(string text) => new MorseText(text.Where(c => c == '.' || c == '-' || c == '/').Cat());
		public MorseText Add(char c, int i) => c == '.' || c == '-' || c == '/' ? new MorseText(Text[..i] + c + Text[i..]) : this;
		public MorseText Remove(int i) => Text.Length > i && i >= 0 ? new MorseText(Text[..i] + Text[(i + 1)..]) : this;
		public MorseText Inverted => new MorseText(Text.Replace('.', '$').Replace('-', '.').Replace('$', '-'));
		public static MorseText Parse(IEnumerable<char> text) => new MorseText(text.Where(c => c == '.' || c == '-' || c == '/').Cat());

		public int Length => Text.Length;
		public override string ToString() => Text.Split('/').Where(word => word.Length > 0).Select(word => MorseToLetter.GetValueOrDefault(word, '?')).Cat();
		public override bool Equals(object obj) => obj is MorseText m && m.Text == Text;
		public override int GetHashCode() => Text.GetHashCode();
	}
}
