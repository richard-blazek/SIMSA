[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Editor), typeof(SIMSA.iOS.Customization.EditorCustomRenderer))]
namespace SIMSA.iOS.Customization
{
    public class EditorCustomRenderer : Xamarin.Forms.Platform.iOS.EditorRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                Control.Layer.BorderColor = new CoreGraphics.CGColor(1f, 1f, 1f);
				Control.Layer.BorderWidth = 1;
            }
        }
    }
}