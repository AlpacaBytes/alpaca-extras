
using AlpacaExtras.Droid.Effects;
using AlpacaExtras.Droid.Helpers;
using AlpacaExtras.Effects;
using Android.Graphics;
using Android.Util;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(AndroidEntryPlaceholderTextEffect), "EntryPlaceholderTextEffect")]
namespace AlpacaExtras.Droid.Effects
{
    public class AndroidEntryPlaceholderTextEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Element is Entry element && Control is FormsEditText control)
            {
                var effect = (EntryPlaceholderTextEffect)Element.Effects.FirstOrDefault(e => e is EntryPlaceholderTextEffect);
                control.SetHeight(element.FontSize.DpToPixels() + ((int)element.FontSize / 2));
                SetFont(effect.FontFamily, effect.FontSize);
                
                control.BeforeTextChanged += (s, e) =>
                {
                    if (e.Start == 0 && e.AfterCount > 0)
                        SetFont(element.FontFamily, (float)element.FontSize);
                };

                control.AfterTextChanged += (s, e) =>
                {
                    if (string.IsNullOrEmpty(control.Text))
                        SetFont(effect.FontFamily, effect.FontSize);
                };

                void SetFont(string fontFamily, float fontSize)
                {
                    if (string.IsNullOrEmpty(fontFamily))
                    {
                        control.Typeface = Typeface.Default;
                    }
                    else if(fontFamily.Contains("#"))
                    {
                        control.Typeface = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, fontFamily.Split('#').FirstOrDefault());
                    }
                    else
                    {
                        control.Typeface = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, fontFamily);
                    }
                    control.TextSize = fontSize;
                }
            }
        }

        protected override void OnDetached()
        {
        }
    }
}