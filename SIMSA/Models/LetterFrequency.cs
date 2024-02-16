namespace SIMSA.Models
{
	public class LetterFrequency
	{
		public char Letter { get; }
		public double Frequency { get; }
		public LetterFrequency(char letter = '\0', double frequency = 0.0)
		{
			Letter = letter;
			Frequency = frequency;
		}
		public override bool Equals(object obj) => obj is LetterFrequency l && (l.Letter, l.Frequency) == (Letter, Frequency);
		public override int GetHashCode() => (Letter, Frequency).GetHashCode();
	}
}
