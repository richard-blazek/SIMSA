using System;
using SIMSA.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIMSA.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlagSemaphore : ContentPage
	{
		void OnTouch(object sender, TouchActionEventArgs args)
		{
			if (args.Type == TouchActionType.Pressed && BindingContext is FlagSemaphoreVM vm)
			{
				vm.SetAngle(Math.Atan2(args.Location.Y - canvas.Height / 2, args.Location.X - canvas.Width / 2));
			}
		}
		void OnPaint(object sender, SKPaintSurfaceEventArgs args)
		{
			var info = args.Info;
			var canvas = args.Surface.Canvas;
			var radius = Math.Min(info.Width, info.Height) / 2f;

			if (BindingContext is FlagSemaphoreVM { Angles: var (angle1, angle2) })
			{
				canvas.Clear();
				canvas.DrawCircle(info.Width / 2f, info.Height / 2f, radius, new SKPaint { Color = new SKColor(255, 255, 255) });
				canvas.DrawLine(info.Width / 2f, info.Height / 2f, MathF.Cos(angle1) * radius + info.Width / 2, MathF.Sin(angle1) * radius + info.Height / 2, new SKPaint { Color = new SKColor(0, 0, 0), StrokeWidth = 3 });
				canvas.DrawLine(info.Width / 2f, info.Height / 2f, MathF.Cos(angle2) * radius + info.Width / 2, MathF.Sin(angle2) * radius + info.Height / 2, new SKPaint { Color = new SKColor(0, 0, 0), StrokeWidth = 3 });
			}
		}
		void VMChangeHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Angles")
			{
				canvas.InvalidateSurface();
			}
		}
		public FlagSemaphore()
		{
			InitializeComponent();
			
			if (BindingContext is FlagSemaphoreVM vm)
			{
				vm.PropertyChanged += VMChangeHandler;
			}
		}
	}
}