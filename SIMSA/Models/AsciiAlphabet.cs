using System.Collections.Immutable;
using SIMSA.Resources;

namespace SIMSA.Models
{
	public class AsciiAlphabet : IAlphabet
	{
		public string Name => AppResources.Ascii;
		public int Count => 128;
		public int ZeroIndex => 0;

		public ImmutableArray<string> ControlCodes = ImmutableArray.Create("[NUL]", "[SOH]", "[STX]", "[ETX]", "[EOT]", "[ENQ]", "[ACK]", "[BEL]", "[BS]", "[HT]", "[LF]", "[VT]", "[FF]", "[CR]", "[SO]", "[SI]", "[DLE]", "[DC1]", "[DC2]", "[DC3]", "[DC4]", "[NAK]", "[SYN]", "[ETB]", "[CAN]", "[EM]", "[SUB]", "[ESC]", "[FS]", "[GS]", "[RS]", "[US]");
		public string this[int idx]
		{
			get
			{
				var i = idx.Mod(128);
				return i == 127 ? "[DEL]" : i < 32 ? ControlCodes[i] : ((char)i).ToString();
			}
		}
		public int IndexOf(string text) => text.Length == 1 && text[0] < 128 ? text[0] : -1;
		public override bool Equals(object obj) => obj is AsciiAlphabet;
		public override int GetHashCode() => 517;
	}
}
