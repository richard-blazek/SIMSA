using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSA.Models
{
	public class FlagSemaphoreText
	{
		static byte MakeValue(int flag1, int flag2) => (byte)(flag1.Mod(8) << 3 | flag2.Mod(8));
		static byte ExtractFlag(byte value, bool second) => (byte)(second ? value & 7 : value >> 3);
		static byte Turn(byte value, int turn) => MakeValue(ExtractFlag(value, false) + turn, ExtractFlag(value, true) + turn);
		static byte Mirror(byte value) => MakeValue(7 - ExtractFlag(value, false), 7 - ExtractFlag(value, true));

		static readonly ImmutableDictionary<byte, char> SemaphoreToLetters = new Dictionary<byte, char>
		{
			{MakeValue(4,5),'A'}, {MakeValue(5,4),'A'},
			{MakeValue(4,6),'B'}, {MakeValue(6,4),'B'},
			{MakeValue(4,7),'C'}, {MakeValue(7,4),'C'},
			{MakeValue(4,0),'D'}, {MakeValue(0,4),'D'},
			{MakeValue(4,1),'E'}, {MakeValue(1,4),'E'},
			{MakeValue(4,2),'F'}, {MakeValue(2,4),'F'},
			{MakeValue(4,3),'G'}, {MakeValue(3,4),'G'},
			{MakeValue(5,6),'H'}, {MakeValue(6,5),'H'},
			{MakeValue(5,7),'I'}, {MakeValue(7,5),'I'},
			{MakeValue(5,0),'K'}, {MakeValue(0,5),'K'},
			{MakeValue(5,1),'L'}, {MakeValue(1,5),'L'},
			{MakeValue(5,2),'M'}, {MakeValue(2,5),'M'},
			{MakeValue(5,3),'N'}, {MakeValue(3,5),'N'},
			{MakeValue(6,7),'O'}, {MakeValue(7,6),'O'},
			{MakeValue(6,0),'P'}, {MakeValue(0,6),'P'},
			{MakeValue(6,1),'Q'}, {MakeValue(1,6),'Q'},
			{MakeValue(6,2),'R'}, {MakeValue(2,6),'R'},
			{MakeValue(6,3),'S'}, {MakeValue(3,6),'S'},
			{MakeValue(7,0),'T'}, {MakeValue(0,7),'T'},
			{MakeValue(7,1),'U'}, {MakeValue(1,7),'U'},
			{MakeValue(7,2),'Y'}, {MakeValue(2,7),'Y'},
			{MakeValue(7,3),'/'}, {MakeValue(3,7),'/'},
			{MakeValue(0,1),'#'}, {MakeValue(1,0),'#'},
			{MakeValue(0,2),'J'}, {MakeValue(2,0),'J'},
			{MakeValue(0,3),'V'}, {MakeValue(3,0),'V'},
			{MakeValue(1,2),'W'}, {MakeValue(2,1),'W'},
			{MakeValue(1,3),'X'}, {MakeValue(3,1),'X'},
			{MakeValue(2,3),'Z'}, {MakeValue(3,2),'Z'}
		}.ToImmutableDictionary();
		readonly ImmutableList<byte> letters;
		public FlagSemaphoreText() => letters = ImmutableList.Create(MakeValue(4, 5));
		FlagSemaphoreText(ImmutableList<byte> letters) => this.letters = letters;
		public FlagSemaphoreText SetFlag(int value) => this[^1, false] == value ? this : new FlagSemaphoreText(letters.SetItem(letters.Count - 1, MakeValue(value, ExtractFlag(letters[^1], false))));
		public FlagSemaphoreText Turn(int turn) => new FlagSemaphoreText(letters.Select(v => Turn(v, turn)).ToImmutableList());
		public FlagSemaphoreText Mirror() => new FlagSemaphoreText(letters.Select(Mirror).ToImmutableList());
		public FlagSemaphoreText Add() => new FlagSemaphoreText(letters.Add(MakeValue(4, 5)));
		public FlagSemaphoreText Pop() => letters.Count > 1 ? new FlagSemaphoreText(letters.RemoveAt(letters.Count - 1)) : this;
		public byte this[Index i, bool second] => ExtractFlag(letters[i], second);
		public override string ToString() => letters.Select(v => SemaphoreToLetters.GetValueOrDefault(v, '?')).Cat();
		public override bool Equals(object obj) => obj is FlagSemaphoreText f && letters.SequenceEqual(f.letters);
		public override int GetHashCode() => letters.HashSequence();
	}
}
