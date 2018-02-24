
using Xamarin.Forms;

namespace AlpacaExtras.Effects
{
    public class EntryPlaceholderTextEffect : RoutingEffect
    {
        public string FontFamily { get; set; }
        public float FontSize { get; set; } = 14;
        public EntryPlaceholderTextEffect() : base("AlpacaExtras.EntryPlaceholderTextEffect")
        {
        }
    }
}
