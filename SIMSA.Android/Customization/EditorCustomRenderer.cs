[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Editor), typeof(SIMSA.Droid.Customization.EditorCustomRenderer))]
namespace SIMSA.Droid.Customization
{
    public class EditorCustomRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public EditorCustomRenderer(Android.Content.Context ctx) : base(ctx) { }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    Control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White);
                }
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
					Control.Background.SetColorFilter(Android.Graphics.Color.White, Android.Graphics.PorterDuff.Mode.SrcAtop);
#pragma warning restore CS0618 // Type or member is obsolete
				}
            }
        }
    }
}