using AlpacaExtras.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(iOSListViewNoSelectionEffect), "NoSelectionEffect")]
namespace AlpacaExtras.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class iOSListViewNoSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is UITableView control)
                control.AllowsSelection = false;
        }

        protected override void OnDetached()
        {

        }
    }
}
