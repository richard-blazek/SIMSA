using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;

namespace SIMSA.Models
{
	public class Alphabets : IReadOnlyList<IAlphabet>
	{
		static readonly ImmutableArray<IAlphabet> std = ImmutableArray.Create<IAlphabet>(new UnicodeAlphabet(), new AsciiAlphabet());
		public ImmutableList<CustomAlphabet> Custom { get; }

		Alphabets(ImmutableList<CustomAlphabet> custom) => Custom = custom;

		public IAlphabet this[int i] => i < Custom.Count ? Custom[i] : std[i - Custom.Count];
		public int Count => Custom.Count + 2;
		public IEnumerator<IAlphabet> GetEnumerator() => Custom.OfType<IAlphabet>().Concat(std).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override string ToString() => JsonSerializer.Serialize(Custom.Select(ab => Tuple.Create(ab.Letters, ab.Name)));
		public static Alphabets Parse(string text)
		{
			var tuples = JsonSerializer.Deserialize<IEnumerable<Tuple<ImmutableArray<string>, string>>>(text);
			return new Alphabets(tuples.Select(pair => new CustomAlphabet(pair.Item1, pair.Item2)).ToImmutableList());
		}
		public static readonly Alphabets Initial = new Alphabets(ImmutableList.Create(CustomAlphabet.English, CustomAlphabet.Greek, CustomAlphabet.Russian));
		public IAlphabet Default => Custom.Count > 0 ? Custom[0] : std[0];
		public Alphabets Add(CustomAlphabet alphabet) => new Alphabets(Custom.Add(alphabet));
		public Alphabets Update(int index, CustomAlphabet newAlphabet) => new Alphabets(Custom.SetItem(index, newAlphabet));
		public Alphabets Remove(int index) => new Alphabets(Custom.RemoveAt(index));
		public override bool Equals(object obj) => obj is Alphabets a && Custom.SequenceEqual(a.Custom);
		public override int GetHashCode() => Custom.HashSequence();
	}
}
