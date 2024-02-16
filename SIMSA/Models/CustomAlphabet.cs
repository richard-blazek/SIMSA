using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SIMSA.Resources;

namespace SIMSA.Models
{
	public class CustomAlphabet : IAlphabet, IReadOnlyCollection<string>
	{
		public ImmutableArray<string> Letters { get; }
		public string Name { get; }
		public int ZeroIndex => 1;
		public CustomAlphabet(ImmutableArray<string> letters, string name)
		{
			Letters = letters;
			Name = name;
		}

		public string this[int i] => Letters[(i - ZeroIndex).Mod(Letters.Length)];
		public int IndexOf(string letter) => Letters.IndexOf(letter) switch { -1 => -1, int i => i + ZeroIndex };
		public int Count => Letters.Length;
		public CustomAlphabet WithName(string name) => new CustomAlphabet(Letters, name);
		public CustomAlphabet WithLetters(ImmutableArray<string> letters) => new CustomAlphabet(letters, Name);
		public CustomAlphabet WithLetters(string letters) => WithLetters(letters.DivideToUnicodeChars().Distinct().ToImmutableArray());
		public override string ToString() => Letters.Cat();
		public IEnumerator<string> GetEnumerator() => (Letters as IEnumerable<string>).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => (Letters as IEnumerable).GetEnumerator();
		public override bool Equals(object obj) => obj is CustomAlphabet c && c.Name == Name && c.Letters.SequenceEqual(Letters);
		public override int GetHashCode() => (Name, Letters.HashSequence()).GetHashCode();

		public static readonly CustomAlphabet English = new CustomAlphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(c => c.ToString()).ToImmutableArray(), AppResources.EnglishAlphabet);
		public static readonly CustomAlphabet Greek = new CustomAlphabet("ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ".Select(c => c.ToString()).ToImmutableArray(), AppResources.GreekAlphabet);
		public static readonly CustomAlphabet Russian = new CustomAlphabet("AБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".Select(c => c.ToString()).ToImmutableArray(), AppResources.RussianAlphabet);
		public static readonly CustomAlphabet Hebrew = new CustomAlphabet("אבגדהוזחטיךלמנסעפצקרשת".Select(c => c.ToString()).ToImmutableArray(), AppResources.HebrewAlphabet);
		
		public static readonly CustomAlphabet Empty = new CustomAlphabet(English.Letters, AppResources.NewAlphabet);
	}
}
