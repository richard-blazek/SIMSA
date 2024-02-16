using System.Text;

namespace SIMSA.Models
{
	public class VigenereText
	{
		public string Text { get; }
		public string Key { get; }
		public bool Minus { get; }
		public IAlphabet Alphabet { get; }
		public VigenereText(IAlphabet alphabet, string text = "", string key = "", bool minus = false)
		{
			Alphabet = alphabet;
			Text = text;
			Key = key;
			Minus = minus;
		}

		public override string ToString()
		{
			if (Key.Length <= 0)
			{
				return Text;
			}
			var result = new StringBuilder(Text.Length);
			foreach (var (textPart, keyPart) in Text.DivideToUnicodeChars().Zip(Key.DivideToUnicodeChars().Forever()))
			{
				int textCode = Alphabet.IndexOf(textPart), keyCode = Alphabet.IndexOf(keyPart);
				_ = result.Append(textCode == -1 || keyCode == -1 ? textPart : Alphabet[textCode + keyCode * (Minus ? -1 : 1)]);
			}
			return result.ToString();
		}
		public VigenereText InvertSign() => new VigenereText(Alphabet, Text, Key, !Minus);
		public VigenereText WithText(string text) => new VigenereText(Alphabet, text, Key, Minus);
		public VigenereText WithKey(string key) => new VigenereText(Alphabet, Text, key, Minus);
		public VigenereText WithAlphabet(IAlphabet newAlphabet) => new VigenereText(newAlphabet, Text, Key, Minus);
		public override bool Equals(object obj) => obj is VigenereText v && (v.Text, v.Key, v.Minus) == (Text, Key, Minus);
		public override int GetHashCode() => (Text, Key, Minus).GetHashCode();
	}
}
