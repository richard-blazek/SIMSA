using System;

namespace SIMSA.Models
{
	public static class ColorCalculations
	{
		static int Max(int a, int b, int c) => a > b ? (a > c ? a : c) : (b > c ? b : c);
		static int Min(int a, int b, int c) => a < b ? (a < c ? a : c) : (b < c ? b : c);
		public static (int, int, int, int) ChromaHueMaxMin(int r, int g, int b)
		{
			int max = Max(r, g, b), min = Min(r, g, b), chroma = max - min;
			int hue = chroma switch
			{
				0 => 0,
				_ when max == r => ((g - b) * 60 / chroma + 360) % 360,
				_ when max == g => (b - r) * 60 / chroma + 120,
				_ => (r - g) * 60 / chroma + 240
			};
			return (chroma, hue, max, min);
		}
		static (int, int, int) ColorOffsets(int chroma, int hue)
		{
			int mid = chroma * (60 - Math.Abs(hue.Mod(120) - 60)) / 60;
			return (hue / 60) switch
			{
				0 => (chroma, mid, 0),
				1 => (mid, chroma, 0),
				2 => (0, chroma, mid),
				3 => (0, mid, chroma),
				4 => (mid, 0, chroma),
				_ => (chroma, 0, mid)
			};
		}
		public static RGBColor RGBColor(int chroma, int hue, int min)
		{
			var (r1, g1, b1) = ColorOffsets(chroma, hue);
			return new RGBColor(r1 + min, g1 + min, b1 + min);
		}
	}
}
