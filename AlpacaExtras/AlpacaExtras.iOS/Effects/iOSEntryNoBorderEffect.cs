
using AlpacaExtras.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(iOSEntryNoBorderEffect), "EntryNoBorderEffect")]
namespace AlpacaExtras.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class iOSEntryNoBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is UITextField control)
                control.BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        {
        }
    }
}
