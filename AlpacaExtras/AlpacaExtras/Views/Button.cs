using System;
using System.Runtime.CompilerServices;
using SkiaSharp.Views.Forms;
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

        public static readonly BindableProperty DisabledTextColorProperty =
            BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(Button), Color.Default,
                propertyChanged: Redraw);

        public static BindableProperty DisabledFillColorProperty =
            BindableProperty.Create(nameof(DisabledFillColor), typeof(Color), typeof(Button), Color.Default,
                propertyChanged: Redraw);

        public static BindableProperty DisabledBorderColorProperty =
            BindableProperty.Create(nameof(DisabledBorderColorProperty), typeof(Color), typeof(Button), Color.Default);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(Command), typeof(Views.Button));
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(Views.Button));


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
                Redraw(this, null, IsEnabled);
        }

        public override Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }
        public new Color BackgroundColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }
        public override Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
        public override Color FillColor { get => (Color)GetValue(FillColorProperty); set => SetValue(FillColorProperty, value); }
        public Color DisabledTextColor { get => (Color)GetValue(DisabledTextColorProperty); set => SetValue(DisabledTextColorProperty, value); }
        public Color DisabledBorderColor { get => (Color)GetValue(DisabledBorderColorProperty); set => SetValue(DisabledBorderColorProperty, value); }
        public Color DisabledFillColor { get => (Color)GetValue(DisabledFillColorProperty); set => SetValue(DisabledFillColorProperty, value); }
        public Command Command { get => (Command)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
        public object CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }

        //public ImageSource LeftIcon { get => (ImageSource)GetValue(LeftIconProperty); set => SetValue(LeftIconProperty, value); }

        public Button()
        {
            OnTapped = () =>
            {
                if (Command != null && Command.CanExecute(CommandParameter))
                    Command.Execute(CommandParameter);
            };
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            if (IsEnabled)
            {
                base.OnPaintSurface(e);
            }
            else
            {
                Paint(e, DisabledFillColor, DisabledBorderColor, DisabledTextColor);
            }
        }
    }
}
