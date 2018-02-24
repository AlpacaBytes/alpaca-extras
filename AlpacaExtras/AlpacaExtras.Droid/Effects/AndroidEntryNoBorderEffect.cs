
using AlpacaExtras.Droid.Effects;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(AndroidEntryNoBorderEffect), "EntryNoBorderEffect")]
namespace AlpacaExtras.Droid.Effects
{
    public class AndroidEntryNoBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is FormsEditText control)
            {
                control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                control.SetPadding(0, 0, 0, 0);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}