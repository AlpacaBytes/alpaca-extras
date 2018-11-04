using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.Effects
{
    [Preserve(AllMembers = true)]
    public class MaxLinesEffect : RoutingEffect
    {
        public int MaxLines { get; set; } = -1;
        public int Lines { get; set; } = 0;

        public MaxLinesEffect() : base("AlpacaExtras.MaxLinesEffect")
        {
        }
    }
}
