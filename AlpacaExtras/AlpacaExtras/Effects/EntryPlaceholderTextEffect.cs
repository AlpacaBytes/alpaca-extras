
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.Effects
{
    [Preserve(AllMembers = true)]
    public class EntryPlaceholderTextEffect : RoutingEffect
    {
        public string FontFamily { get; set; }
        public float FontSize { get; set; } = 14;
        public EntryPlaceholderTextEffect() : base("AlpacaExtras.EntryPlaceholderTextEffect")
        {
        }
    }
}
