using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class ToggleButton : BaseButton
    {

        public ToggleButton()
        {
            OnTapped = () => IsToggled = !IsToggled;
        }

        public static BindableProperty OnTextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ToggleButton));
        public static BindableProperty OffTextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ToggleButton));


        public bool IsToggled { get => (bool)GetValue(IsToggledProperty); set => SetValue(IsToggledProperty, value); }

        public string OnText { get => (string)GetValue(OnTextProperty); set => SetValue(OnTextProperty, value); }
        public Color OnTextColor { get => (Color)GetValue(OnTextColorProperty); set => SetValue(OnTextColorProperty, value); }
        public Color OnFillColor { get => (Color)GetValue(OnFillColorProperty); set => SetValue(OnFillColorProperty, value); }
        public Color OnBorderColor { get => (Color)GetValue(OnBorderColorProperty); set => SetValue(OnBorderColorProperty, value); }

        public string OffText { get => (string)GetValue(OffTextProperty); set => SetValue(OffTextProperty, value); }
        public Color OffTextColor { get => (Color)GetValue(OffTextColorProperty); set => SetValue(OffTextColorProperty, value); }
        public Color OffFillColor { get => (Color)GetValue(OffFillColorProperty); set => SetValue(OffFillColorProperty, value); }
        public Color OffBorderColor { get => (Color)GetValue(OffBorderColorProperty); set => SetValue(OffBorderColorProperty, value); }

        public override string Text
        {
            get
            {
                if (IsToggled)
                    return OnText ?? base.Text;
                return OffText ?? base.Text;
            }
            set => base.Text = value;
        }
        public override Color BorderColor { get => IsToggled ? OnBorderColor : OffBorderColor; set { } }
        public override Color TextColor { get => IsToggled ? OnTextColor : OffTextColor; set { } }
        public override Color FillColor { get => IsToggled ? OnFillColor : OffFillColor; set { } }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleButton), false,
         propertyChanged: Redraw, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty OnTextColorProperty =
            BindableProperty.Create(nameof(OnTextColor), typeof(Color), typeof(Button), Color.Default,
                            propertyChanged: Redraw);

        public static BindableProperty OnFillColorProperty =
            BindableProperty.Create(nameof(OnFillColor), typeof(Color), typeof(Button), Color.Default,
                propertyChanged: Redraw);

        public static BindableProperty OnBorderColorProperty =
            BindableProperty.Create(nameof(OnBorderColor), typeof(Color), typeof(Button), Color.Default);

        public static readonly BindableProperty OffTextColorProperty =
            BindableProperty.Create(nameof(OffTextColor), typeof(Color), typeof(Button), Color.Default,
                    propertyChanged: Redraw);

        public static BindableProperty OffFillColorProperty =
            BindableProperty.Create(nameof(OffFillColor), typeof(Color), typeof(Button), Color.Default,
                propertyChanged: Redraw);

        public static BindableProperty OffBorderColorProperty =
            BindableProperty.Create(nameof(OffBorderColor), typeof(Color), typeof(Button), Color.Default);
    }
}
