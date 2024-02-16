using System;
using System.Linq;

namespace SIMSA.Models
{
	public class NumericText
	{
		public static readonly string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		static byte DigitValue(char c) => c >= '0' && c <= '9' ? (byte)(c - '0') : (byte)(c >= 'A' && c <= 'Z' ? c - 'A' + 10 : 255);

		string CodeToText(string s, IAlphabet alphabet)
		{
			if (s.Length <= 0)
			{
				return string.Empty;
			}
			int value = 0;
			for (int i = 0; i < s.Length; ++i)
			{
				value *= Radix;
				byte digit = DigitValue(s[i]);
				if (digit >= Radix)
				{
					return string.Empty;
				}
				value += digit;
			}
			return alphabet[value];
		}
		static bool IsValidChar(char c, byte radix) => c == ',' || DigitValue(c) < radix;

		public string Text { get; }
		public byte Radix { get; }
		public IAlphabet Alphabet { get; }

		NumericText(string text, byte radix, IAlphabet alphabet) => (Text, Radix, Alphabet) = (text, radix, alphabet);
		public NumericText(IAlphabet alphabet) : this(string.Empty, 10, alphabet) { }
		public NumericText WithAlphabet(IAlphabet abc) => new NumericText(Text, Radix, abc);
		public NumericText WithRadix(byte radix)
		{
			radix = Math.Clamp(radix, (byte)2, (byte)36);
			return new NumericText(Text.Where(c => IsValidChar(c, radix)).Cat(), radix, Alphabet);
		}
		public NumericText WithText(string text) => new NumericText(text.Where(c => IsValidChar(c, Radix)).Cat(), Radix, Alphabet);

		public NumericText Add(char c, int i) => c == ',' || DigitValue(c) < Radix ? WithText(Text[..i] + c + Text[i..]) : this;
		public NumericText Remove(int i) => i >= 0 && i < Text.Length ? WithText(Text[..i] + Text[(i + 1)..]) : this;
		public NumericText Invert() => WithText(Text.Select(c => c == ',' ? ',' : Digits[Radix - 1 - DigitValue(c)]).Cat());

		public int Length => Text.Length;
		public override string ToString() => Text.Split(',').Select(code => CodeToText(code, Alphabet)).Cat();
		public override bool Equals(object obj) => obj is NumericText n && (n.Text, n.Radix, n.Alphabet) == (Text, Radix, Alphabet);
		public override int GetHashCode() => (Text, Radix, Alphabet).GetHashCode();
		public bool IsTextValid(string text) => text.All(c => c == ',' || DigitValue(c) < Radix);
	}
}
