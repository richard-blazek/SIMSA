using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xamarin.Forms;

namespace SIMSA
{
	public static class Extensions
	{
		public static void SetText(this Entry entry, string text, int position)
		{
			entry.Text = text;

			// If you do not unfocus it, Effects.HideKeyboard may not work and the keyboard might appear
			// It happens only sometimes, but this call prevents it
			entry.Unfocus();

			// You may think that this code does two times the same thing and I thought so as well
			// However, if you remove one of the two lines, the cursor position will be set to the end
			// My theory is that the first line causes re-focusing with cursor at the end
			// and the second line moves the cursor
			entry.CursorPosition = Math.Clamp(position, 0, text.Length);
			entry.CursorPosition = Math.Clamp(position, 0, text.Length);
		}

		public static string Cat<T>(this IEnumerable<T> e, string str = "") => string.Join(str, e.Select(it => it?.ToString() ?? ""));
		public static string Cat(this IEnumerable<string> e, string str = "") => string.Join(str, e);
		public static int Mod(this int n, int d) => (n % d + d) % d;
		public static IEnumerable<T> Range<T>(this int n, Func<int, T> fn) => Enumerable.Range(0, n).Select(fn);
		public static void Range(this int n, Action<int> action)
		{
			for (int i = 0; i < n; ++i)
			{
				action(i);
			}
		}
		public static IEnumerable<string> DivideToUnicodeChars(this string s)
		{
			for (int i = 0; i < s.Length; ++i)
			{
				if (char.IsHighSurrogate(s[i]))
				{
					yield return s[i].ToString() + s[i + 1];
					++i;
				}
				else
				{
					yield return s[i].ToString();
				}
			}
		}
		public static IEnumerable<T> Forever<T>(this IEnumerable<T> e)
		{
			for (; ; )
			{
				foreach (var it in e)
				{
					yield return it;
				}
			}
		}
		public static IEnumerable<(T First, U Second)> Zip<T, U>(this IEnumerable<T> f, IEnumerable<U> s) => f.Zip(s, (a, b) => (a, b));
		public static ImmutableArray<int> AllLowerPrimes(this int limit)
		{
			bool[] compound = new bool[limit - 1];
			for (int number = 2; number * number <= limit; ++number)
			{
				if (!compound[number - 2])
				{
					for (int i = number * number; i <= limit; i += number)
					{
						compound[i - 2] = true;
					}
				}
			}
			var primes = ImmutableArray.CreateBuilder<int>();
			for (int i = 0, len = compound.Length; i < len; ++i)
			{
				if (!compound[i])
				{
					primes.Add(i + 2);
				}
			}
			return primes.ToImmutable();
		}

		static readonly string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		static string Digit(long i) => i >= 36 ? $"({i})" : Digits[(int)i].ToString();
		static long DigitValue(char c) => c >= '0' && c <= '9' ? c - '0' : c >= 'A' && c <= 'Z' ? c - 'A' + 10 : int.MaxValue;
		public static string ToString(this long num, long radix)
		{
			if (num == 0)
			{
				return "0";
			}
			if (radix < 2)
			{
				return "";
			}
			bool negative = num < 0;
			num = negative ? -num : num;
			string result = "";
			while (num > 0)
			{
				result = Digit(num % radix) + result;
				num /= radix;
			}
			return (negative ? "-" : "") + result;
		}
		public static string ToString(this int num, int radix) => ToString(num, radix);
		public static bool TryParse(this string str, long radix, out long value)
		{
			value = 0;
			bool negative = str.Length > 0 && str[0] == '-';
			bool correct = str.Length >= (negative ? 1 : 0) && radix >= 2;
			for (int i = negative ? 1 : 0; i < str.Length && correct; ++i)
			{
				long digit = DigitValue(str[i]);
				correct &= digit < radix && value < (long.MaxValue - digit) / radix;
				value = digit + value * radix;
			}
			value = negative ? -value : value;
			return correct;
		}
		public static int ParseInt(this string str, int radix = 10)
		{
			_ = str.TryParse(radix, out long v);
			return (int)v;
		}
		static int CombineHashCodes(int h1, int h2) => ((h1 << 5) + h1) ^ h2;
		public static int HashSequence(this IEnumerable seq)
		{
			int result = 0;
			foreach (var item in seq)
			{
				result = CombineHashCodes(result, item.GetHashCode());
			}
			return result;
		}
		public static void ForEach<T>(this IEnumerable<T> e, Action<T> action)
		{
			foreach (var item in e)
			{
				action(item);
			}
		}
		public static void ForEach<T>(this IEnumerable e, Action<T> action)
		{
			foreach (var item in e.OfType<T>())
			{
				action(item);
			}
		}
		public static IEnumerable<T> Align<T>(this IEnumerable<T> e, int length, T value)
		{
			int i = 0;
			var iterator = e.GetEnumerator();
			while (iterator.MoveNext() && i < length)
			{
				yield return iterator.Current;
				++i;
			}
			while (i < length)
			{
				yield return value;
				++i;
			}
		}
		public static string RemoveNonDigits(this string s, int radix = 10) => s.ToUpper().Where(c => (c >= '0' && c <= '9' && c < '0' + radix) || (c >= 'A' && c <= 'Z' && c < 'A' + radix - 10)).Cat();
		public static int Find<T>(this IReadOnlyList<T> list, Func<T, bool> predicate)
		{
			for (int i = 0, len = list.Count; i < len; ++i)
			{
				if (predicate(list[i]))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
