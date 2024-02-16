using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSA.Models
{
	public class HTMLColorModel : IColorModel
	{
		public ImmutableArray<string> Names => ImmutableArray.Create("HTML");
		public bool NumbersOnly => false;
		static byte ParseHex(string s) => byte.Parse(s, System.Globalization.NumberStyles.HexNumber);
		public (ImmutableArray<string>, RGBColor) Parse(IReadOnlyList<string> inputs)
		{
			var input = inputs.FirstOrDefault()?.RemoveNonDigits(16) ?? string.Empty;
			return (ImmutableArray.Create(input), input.Length switch
			{
				3 => new RGBColor(ParseHex(input[..1]) * 17, ParseHex(input[1..2]) * 17, ParseHex(input[2..]) * 17),
				6 => new RGBColor(ParseHex(input[..2]), ParseHex(input[2..4]), ParseHex(input[4..])),
				_ => new RGBColor(0, 0, 0),
			});
		}
		public ImmutableArray<string> ToStrings(RGBColor color) => ImmutableArray.Create(color.ToString());
	}
}
