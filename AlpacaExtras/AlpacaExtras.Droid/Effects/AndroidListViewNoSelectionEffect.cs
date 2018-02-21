
using AlpacaExtras.Droid.Effects;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(AndroidListViewNoSelectionEffect), "NoSelectionEffect")]
namespace AlpacaExtras.Droid.Effects
{
    public class AndroidListViewNoSelectionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is Android.Widget.ListView control)
            {
                //These 2 lines are enough to disable item selection and prevents it from changing item color to orange.
                control.ItemClick += (s, ev) => { };
                control.SetSelector(Android.Resource.Color.Transparent);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}