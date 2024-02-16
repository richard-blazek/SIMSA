using Xamarin.Forms;

namespace SIMSA.Models
{
	public class RGBColor
	{
		public readonly byte R, G, B;
		public RGBColor(int r, int g, int b) => (R, G, B) = ((byte)r, (byte)g, (byte)b);
		public Color ToXamarin => new Color(R / 255.0, G / 255.0, B / 255.0);
		public override string ToString() => $"{R:X2}{G:X2}{B:X2}";
	}
}