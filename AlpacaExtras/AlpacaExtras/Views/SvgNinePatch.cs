using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class SvgNinePatch : SKCanvasView
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(string), typeof(SvgNinePatch), propertyChanged: OnSourceChanged);


        public static readonly BindableProperty InsetsProperty =
            BindableProperty.Create("Insets", typeof(Thickness), typeof(SvgNinePatch), new Thickness(), propertyChanged: OnInsetsChanged);

        SKBitmap bmp;

        public string Source { get => (string)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
        public Thickness Insets { get => (Thickness)GetValue(InsetsProperty); set => SetValue(InsetsProperty, value); }

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            string source = newValue as string;
            var view = (SvgNinePatch)bindable;

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
                    view.bmp = null;
                    return;
                }

                using (var resource = AlpacaExtras.AssetsAssembly.GetManifestResourceStream(path))
                {
                    var svg = new SkiaSharp.Extended.Svg.SKSvg();
                    svg.Load(resource);

                    var bitmap = new SKBitmap((int)svg.CanvasSize.Width, (int)svg.CanvasSize.Height);
                    using (var canvas = new SKCanvas(bitmap))
                    {
                        canvas.DrawPicture(svg.Picture);
                        view.bmp = bitmap;
                    }
                }
            }

            view.InvalidateSurface();
        }

        private static void OnInsetsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (SvgNinePatch)bindable;
            view.InvalidateSurface();
        }


        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            if (bmp == null)
                return;

            var canvas = e.Surface.Canvas;
            canvas.Scale(e.Info.Width / (float)Width);
            var paint = new SKPaint
            {
                IsAntialias = true
            };


            canvas.Clear();
            canvas.DrawBitmapNinePatch(bmp, new SKRectI((int)Insets.Left, (int)Insets.Top, bmp.Width - (int)Insets.Right, bmp.Height - (int)Insets.Bottom), new SKRect(0, 0, (float)Width, (float)Height), paint);
        }

        
    }
}
