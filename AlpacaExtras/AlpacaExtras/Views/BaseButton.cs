using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public abstract class BaseButton : SKCanvasView
    {
        public static BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Button),
                                    propertyChanged: OnTextChanged);
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
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Button), new Thickness(5, 2));

        public static BindableProperty EdgeTypeProperty =
            BindableProperty.Create(nameof(EdgeType), typeof(EdgeType), typeof(Button), EdgeType.Rounded,
                propertyChanged: Redraw);

        public static BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(float), typeof(Button), 0f,
            propertyChanged: Redraw);

        public virtual string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public string FontFamily { get => (string)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }
        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
        public Thickness Padding { get => (Thickness)GetValue(PaddingProperty); set => SetValue(PaddingProperty, value); }
        public float BorderThickness { get => (float)GetValue(BorderThicknessProperty); set => SetValue(BorderThicknessProperty, value); }
        public EdgeType EdgeType { get => (EdgeType)GetValue(EdgeTypeProperty); set => SetValue(EdgeTypeProperty, value); }
        public float BorderRadius { get => (float)GetValue(BorderRadiusProperty); set => SetValue(BorderRadiusProperty, value); }

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

        protected static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((BaseButton)bindable).InvalidateMeasure();
            Redraw(bindable, oldValue, newValue);
        }

        protected static void Redraw(BindableObject bindable, object oldValue, object newValue)
        {
            ((SKCanvasView)bindable).InvalidateSurface();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return base.OnMeasure(widthConstraint, heightConstraint);
            }
            else
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
        }

        protected void Paint(SKPaintSurfaceEventArgs e, Color fillColor, Color borderColor, Color textColor)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var scale = e.Info.Width / (float)Width;
                var adjustedHeight = (float)Height;
                var adjustedWidth = (float)Width;
                var adjustedBorder = BorderThickness;
                float borderRadius = 0;
                if (BorderRadius > 0)
                {
                    borderRadius = BorderRadius;
                }
                else
                {
                    borderRadius = adjustedHeight / 2;
                }

                base.OnPaintSurface(e);
                var canvas = e.Surface.Canvas;
                canvas.Clear();

                canvas.Scale(e.Info.Width / (float)Width);

                // Draw the background
                if (fillColor != Color.Default)
                {
                    var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = fillColor.ToSKColor(),
                        IsAntialias = true
                    };
                    if (EdgeType == EdgeType.Rounded)
                        canvas.DrawRoundRect(new SKRect(0, 0, adjustedWidth, adjustedHeight), borderRadius, borderRadius, paint);
                    else if (EdgeType == EdgeType.Square)
                        canvas.DrawRect(new SKRect(0, 0, adjustedWidth, adjustedHeight), paint);
                }

                // Draw the border
                if (borderColor != Color.Default)
                {
                    var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = borderColor.ToSKColor(),
                        IsAntialias = true,
                        StrokeWidth = adjustedBorder
                    };
                    if (EdgeType == EdgeType.Rounded)
                        canvas.DrawRoundRect(new SKRect(adjustedBorder / 2, adjustedBorder / 2, adjustedWidth - adjustedBorder / 2, adjustedHeight - adjustedBorder / 2), borderRadius, borderRadius, paint);
                    else if (EdgeType == EdgeType.Square)
                        canvas.DrawRect(new SKRect(0, 0, adjustedWidth, adjustedHeight), paint);
                }

                var textPaint = new SKPaint
                {
                    TextSize = (float)FontSize,
                    Color = textColor == Color.Default ? Color.Black.ToSKColor() : textColor.ToSKColor(),
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Left
                };

                if (FontFamily != null && AlpacaExtras.Assets.ContainsKey(FontFamily))
                {
                    var font = AlpacaExtras.Assets[FontFamily];
                    textPaint.Typeface = SKTypeface.FromData(SKData.CreateCopy(font.Value));
                }

                SKRect textBounds = new SKRect();
                textPaint.MeasureText(Text, ref textBounds);


                canvas.DrawText(Text, adjustedWidth / 2 - textBounds.MidX, adjustedHeight / 2 - textBounds.MidY, textPaint);
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            Paint(e, FillColor, BorderColor, TextColor);
        }
    }
}
