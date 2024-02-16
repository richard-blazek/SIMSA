using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSA.Models
{
	public class RGBColorModel : IColorModel
	{
		public ImmutableArray<string> Names => ImmutableArray.Create("R", "G", "B");
		public bool NumbersOnly => true;
		public (ImmutableArray<string>, RGBColor) Parse(IReadOnlyList<string> inputs)
		{
			var texts = inputs.Align(3, "0").Select(s => s.RemoveNonDigits()).ToImmutableArray();
			return (texts, new RGBColor(Math.Clamp(texts[0].ParseInt(), 0, 255), Math.Clamp(texts[1].ParseInt(), 0, 255), Math.Clamp(texts[2].ParseInt(), 0, 255)));
		}
		public ImmutableArray<string> ToStrings(RGBColor color) => ImmutableArray.Create(color.R.ToString(), color.G.ToString(), color.B.ToString());
	}
}
