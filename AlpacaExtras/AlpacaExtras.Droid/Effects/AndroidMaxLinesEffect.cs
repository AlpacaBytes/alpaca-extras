using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Runtime;
using AlpacaExtras.Droid.Effects;
using AlpacaExtras.Effects;
using System.Linq;
using static Android.Text.TextUtils;

[assembly: ExportEffect(typeof(AndroidMaxLinesEffect), "MaxLinesEffect")]
namespace AlpacaExtras.Droid.Effects
{
    [Preserve(AllMembers = true)]
    class AndroidMaxLinesEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var et = Control as TextView;
            if (et == null)
                return;

            var effect = (MaxLinesEffect)Element.Effects.FirstOrDefault(e => e is MaxLinesEffect);

            //et.SetMaxLines(effect.MaxLines > 0 ? effect.MaxLines : int.MaxValue);

            //if (effect.Lines > 0)
            et.SetSingleLine(false);
                et.SetMaxLines(2);
        }

        protected override void OnDetached()
        {

        }
    }
}