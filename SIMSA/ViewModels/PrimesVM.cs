using System.Collections.Immutable;
using SIMSA.Models;

namespace SIMSA.ViewModels
{
	public class PrimesVM : ViewModelBase
	{
		int upperBound;
		ImmutableArray<int> primes = ImmutableArray<int>.Empty;
		public string Output => primes.Cat(", ");
		public int Count => primes.Length;
		public int UpperBound
		{
			get => upperBound;
			set
			{
				primes = value.AllLowerPrimes();
				ChangeProperty(ref upperBound, value, "UpperBound", "Output", "Count");
			}
		}
		public PrimesVM() { }
	}
}
