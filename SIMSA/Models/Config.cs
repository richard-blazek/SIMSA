using System.Collections.Generic;

namespace SIMSA.Models
{
	public class Config
	{
		public Alphabets Alphabets { get; }
		public Config(Alphabets alphabets) => Alphabets = alphabets;
		public static readonly Config Initial = new Config(Alphabets.Initial);
		public IAlphabet DefaultAlphabet => Alphabets.Default;
		public Config With(Alphabets alphabets) => new Config(alphabets);
		public Config Add(CustomAlphabet alphabet) => With(Alphabets.Add(alphabet));

		public override string ToString() => Alphabets.ToString();
		public override bool Equals(object obj) => obj is Config c && Alphabets.Equals(c.Alphabets);
		public override int GetHashCode() => Alphabets.GetHashCode();
		public static Config Parse(string from) => new Config(Alphabets.Parse(from));
		public void Save(IDictionary<string, object> dict, string key) => dict[key] = ToString();
		public static Config Load(IDictionary<string, object> dict, string key)
		{
			if (dict.ContainsKey(key))
			{
				try
				{
					return Parse((string)dict[key]);
				}
				catch (System.Text.Json.JsonException) { }
			}
			dict[key] = Initial.ToString();
			return Initial;
		}
	}
}
