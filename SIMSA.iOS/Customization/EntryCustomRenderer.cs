[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(SIMSA.iOS.Customization.EntryCustomRenderer))]
namespace SIMSA.iOS.Customization
{
	public class EntryCustomRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;

                Control.Layer.AddSublayer(new CoreAnimation.CALayer
                {
                    BorderColor = UIKit.UIColor.FromRGB(255, 255, 255).CGColor,
                    BackgroundColor = UIKit.UIColor.FromRGB(255, 255, 255).CGColor,
                    Frame = new CoreGraphics.CGRect(0, Frame.Height / 2, Frame.Width * 2, 1f)
                });
            }            
        }
    }
}