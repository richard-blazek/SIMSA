using System;
using System.Windows.Input;
using SIMSA.Models;
using Xamarin.Forms;

namespace SIMSA.ViewModels
{
	public class FlagSemaphoreVM : ViewModelBase
	{
		FlagSemaphoreText text = new FlagSemaphoreText();
		public ICommand Backspace { get; }
		public ICommand Confirm { get; }
		public ICommand Mirror { get; }
		public ICommand Turn { get; }
		public string Output => text.ToString();
		public (float, float) Angles => ((text[^1, false] - 2) * MathF.PI / 4, (text[^1, true] - 2) * MathF.PI / 4);
		public FlagSemaphoreVM()
		{
			Backspace = new Command(() => ChangeProperty(ref text, text.Pop(), "Output", "Angles"));
			Confirm = new Command(() => ChangeProperty(ref text, text.Add(), "Output", "Angles"));
			Mirror = new Command(() => ChangeProperty(ref text, text.Mirror(), "Output", "Angles"));
			Turn = new Command(() => ChangeProperty(ref text, text.Turn(1), "Output", "Angles"));
		}
		public void SetAngle(double angle) => ChangeProperty(ref text, text.SetFlag(((int)Math.Round(angle * 4 / Math.PI + 2)).Mod(8)), "Output", "Angles");
	}
}
