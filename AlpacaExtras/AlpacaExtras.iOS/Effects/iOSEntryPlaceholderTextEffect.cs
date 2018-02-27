using AlpacaExtras.Effects;
using AlpacaExtras.iOS.Effects;
using Foundation;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(iOSEntryPlaceholderTextEffect), "EntryPlaceholderTextEffect")]
namespace AlpacaExtras.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class iOSEntryPlaceholderTextEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element is Entry element && Control is UITextField control)
            {
                var effect = (EntryPlaceholderTextEffect)Element.Effects.FirstOrDefault(e => e is EntryPlaceholderTextEffect);
                SetPlaceholderText();
                element.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == element.Placeholder)
                        SetPlaceholderText();
                };

                void SetPlaceholderText()
                {
                    if (!string.IsNullOrEmpty(element.Placeholder) && 
                        !string.IsNullOrEmpty(effect?.FontFamily))
                    {
                        control.AttributedPlaceholder = new NSAttributedString(element.Placeholder,
                                    UIFont.FromName(effect.FontFamily, effect.FontSize),
                                    element.PlaceholderColor.ToUIColor());
                    }
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
