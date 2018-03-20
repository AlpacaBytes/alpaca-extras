using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class NinePatch : SKCanvasView
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(string), typeof(NinePatch), propertyChanged: OnSourceChanged);

        public static readonly BindableProperty InsetsProperty =
            BindableProperty.Create("Insets", typeof(Thickness), typeof(NinePatch), new Thickness(), propertyChanged: OnInsetsChanged);

        public static readonly BindableProperty AssetScaleProperty =
            BindableProperty.Create("AssetScale", typeof(double), typeof(NinePatch), 1.0, propertyChanged: OnInsetsChanged);

        SKBitmap bmp;

        public string Source { get => (string)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
        public Thickness Insets { get => (Thickness)GetValue(InsetsProperty); set => SetValue(InsetsProperty, value); }
        public double AssetScale { get => (double)GetValue(AssetScaleProperty); set => SetValue(AssetScaleProperty, value); }

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            string source = newValue as string;
            var view = (NinePatch)bindable;

            if (string.IsNullOrEmpty(source))
                view.bmp = null;
            else
            {
                var parts = new List<string>();
                parts.Add(AlpacaExtras.AssetsAssembly.GetName().Name);
                if (AlpacaExtras.AssetFolder != null)
                    parts.Add(AlpacaExtras.AssetFolder);
                parts.Add(source);

                var path = string.Join(".", parts);

                if (!AlpacaExtras.AssetsAssembly.GetManifestResourceNames().Contains(path))
                {
                    Debug.WriteLine("Alpaca Extras Asset not Found: " + path);

                    view.bmp = null;
                    return;
                }

                using (var resource = AlpacaExtras.AssetsAssembly.GetManifestResourceStream(path))
                using (var stream = new SKManagedStream(resource))
                {
                    view.bmp = SKBitmap.Decode(stream);
                }
            }

            view.InvalidateSurface();
        }

        private static void OnInsetsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (NinePatch)bindable;
            view.InvalidateSurface();
        }


        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            if (bmp == null)
                return;

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            if (bmp == null)
                return;

            var scale = e.Info.Width / (float)Width;

            //canvas.Scale(scale);
            var paint = new SKPaint
            {
                IsAntialias = true
            };

            SKBitmap resizedBitmap = null;

            if (scale == AssetScale)
                resizedBitmap = bmp;
            else
            {
                resizedBitmap = bmp.Resize(new SKImageInfo
                {
                    Height = ((int)(bmp.Height * (scale / AssetScale))),
                    Width = ((int)(bmp.Width * (scale / AssetScale))),
                    AlphaType = bmp.AlphaType,
                    ColorSpace = bmp.ColorSpace,
                    ColorType = bmp.ColorType
                }, SKBitmapResizeMethod.Triangle);
            }

            int left = (int)(Insets.Left * (scale / AssetScale));
            int top = (int)(Insets.Top * (scale / AssetScale));
            int right = resizedBitmap.Width - (int)((Insets.Right) * (scale / AssetScale));
            int bottom = resizedBitmap.Height - (int)(((int)Insets.Bottom) * (scale / AssetScale));

            canvas.DrawBitmapNinePatch(resizedBitmap, new SKRectI(left, top, right, bottom), new SKRect(0, 0, (float)Width * scale, (float)Height * scale), paint);
        }


    }
}
