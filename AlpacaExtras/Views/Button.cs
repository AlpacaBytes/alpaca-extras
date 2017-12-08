using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.Views
{
    [Preserve(AllMembers = true)]
    public class Button : BaseButton
    {
        public static readonly BindableProperty TextColorProperty =
    BindableProperty.Create("TextColor", typeof(Color), typeof(Button), Color.Default,
                            propertyChanged: Redraw);

        public static BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(Button), Color.Default,
                propertyChanged: Redraw);

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColorProperty), typeof(Color), typeof(Button), Color.Default);


        //public static BindableProperty LeftIconProperty =
        //    BindableProperty.Create(nameof(LeftIcon), typeof(ImageSource), typeof(AlpacaButton));


        public override Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }
        public new Color BackgroundColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }
        public override Color BorderColor{ get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
        public override Color FillColor { get => (Color)GetValue(FillColorProperty); set => SetValue(FillColorProperty, value); }

        //public ImageSource LeftIcon { get => (ImageSource)GetValue(LeftIconProperty); set => SetValue(LeftIconProperty, value); }




    }
}
