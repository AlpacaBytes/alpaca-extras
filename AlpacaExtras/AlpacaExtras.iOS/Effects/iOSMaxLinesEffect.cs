using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using AlpacaExtras.iOS.Effects;
using AlpacaExtras.Effects;

[assembly: ExportEffect(typeof(iOSMaxLinesEffect), "MaxLinesEffect")]
namespace AlpacaExtras.iOS.Effects
{
    [Preserve(AllMembers = true)]
    class iOSMaxLinesEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = Control as UILabel;
            if (label == null)
                return;

            var effect = (MaxLinesEffect)Element.Effects.FirstOrDefault(e => e is MaxLinesEffect);

            label.LineBreakMode = UILineBreakMode.TailTruncation;
            var maxLines = effect.MaxLines;
            var lines = effect.Lines;


            if (lines > 0)
            {
                label.Lines = lines;
            }
            else if (maxLines > 0)
            {
                label.Lines = maxLines != -1 ? maxLines : 0;
            }
            else
            {
                label.LineBreakMode = UILineBreakMode.WordWrap;
                label.Lines = -1;
            }

        }

        protected override void OnDetached()
        {
        }
    }
}
