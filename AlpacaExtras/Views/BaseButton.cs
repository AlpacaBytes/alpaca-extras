using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public abstract class BaseButton : SKCanvasView
    {
        public static BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Button));
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(Button), default(Font),
                                    propertyChanged: Redraw);

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(Button), default(string),
                                    propertyChanged: Redraw);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(Button), 14.0,
                                    propertyChanged: Redraw);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(Padding), typeof(float), typeof(Button), 1f);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Button), new Thickness(5,2));

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public string FontFamily { get => (string)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }
        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
        public Thickness Padding { get => (Thickness)GetValue(PaddingProperty); set => SetValue(PaddingProperty, value); }
        public float BorderThickness { get => (float)GetValue(BorderThicknessProperty); set => SetValue(BorderThicknessProperty, value); }

        public virtual Color BorderColor { get; set; }
        public virtual Color TextColor { get; set; }
        public virtual Color FillColor { get; set; }

        protected Action OnTapped { get; set; }

        public BaseButton()
        {
            var tgr = new TapGestureRecognizer
            {
                Command = new Command(() => OnTapped?.Invoke())
            };

            GestureRecognizers.Add(tgr);
        }

        protected static void Redraw(BindableObject bindable, object oldValue, object newValue)
        {
            ((SKCanvasView)bindable).InvalidateSurface();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var paint = new SKPaint
            {
                TextSize = (float)FontSize,
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
            };

            if (FontFamily != null)
            {
                if (AlpacaExtras.Assets.ContainsKey(FontFamily))
                {
                    var font = AlpacaExtras.Assets[FontFamily];
                    paint.Typeface = SKTypeface.FromData(SKData.CreateCopy(font.Value));
                }
            }

            SKRect textBounds = new SKRect();
            var textWidth = paint.MeasureText(Text, ref textBounds);

            var size = new Size(textWidth + Padding.HorizontalThickness, textBounds.Height + Padding.VerticalThickness);
            var request = new SizeRequest(size);

            return request;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var adjustedHeight = (float)Height; // / AlpacaExtras.Scale;
            var adjustedWidth = (float)Width; /// AlpacaExtras.Scale;
            var adjustedBorder = BorderThickness / AlpacaExtras.Scale;

            base.OnPaintSurface(e);
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            canvas.Scale(AlpacaExtras.Scale);

            // Draw the background
            if (FillColor != Color.Default)
            {
                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = FillColor.ToSKColor(),
                    IsAntialias = true
                };
                canvas.DrawRoundRect(new SKRect(0, 0, adjustedWidth, adjustedHeight), adjustedHeight / 2, adjustedHeight / 2, paint);
            }

            // Draw the border
            if (BorderColor != Color.Default)
            {
                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = BorderColor.ToSKColor(),
                    IsAntialias = true,
                    StrokeWidth = adjustedBorder
                };
                canvas.DrawRoundRect(new SKRect(adjustedBorder / 2, adjustedBorder / 2, adjustedWidth - adjustedBorder / 2, adjustedHeight - adjustedBorder / 2), adjustedHeight / 2, adjustedHeight / 2, paint);
            }

            // Draw the text
            if (Text != null)
            {
                var paint = new SKPaint
                {
                    TextSize = (float)FontSize,
                    Color = TextColor == Color.Default ? Color.Black.ToSKColor() : TextColor.ToSKColor(),
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Left
                };

                if (FontFamily != null && AlpacaExtras.Assets.ContainsKey(FontFamily))
                {
                    var font = AlpacaExtras.Assets[FontFamily];
                    paint.Typeface = SKTypeface.FromData(SKData.CreateCopy(font.Value));
                }

                SKRect textBounds = new SKRect();
                paint.MeasureText(Text, ref textBounds);


                canvas.DrawText(Text, (float)Padding.Left, adjustedHeight / 2 - textBounds.MidY, paint);
            }
        }
    }
}
