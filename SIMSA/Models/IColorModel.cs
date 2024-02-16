using System.Collections.Generic;
using System.Collections.Immutable;

namespace SIMSA.Models
{
	public interface IColorModel
	{
		ImmutableArray<string> Names { get; }
		string Name => Names.Cat();
		bool NumbersOnly { get; }
		(ImmutableArray<string>, RGBColor) Parse(IReadOnlyList<string> inputs);
		ImmutableArray<string> ToStrings(RGBColor color);
	}
}
