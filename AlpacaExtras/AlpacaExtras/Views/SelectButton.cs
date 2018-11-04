using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.Views
{
    [Preserve(AllMembers = true)]
    public class SelectButton : ToggleButton
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(object), typeof(SelectButton));

        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        public SelectButton()
        {
            OnTapped = () =>
            {
                if (Parent is SelectionGroup group)
                {
                    group.SelectValue(Value);
                }
            };
        }
    }
}
