using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AlpacaExtras;
using System.IO;

namespace AlpacaExtrasDemo.Droid
{
    [Activity(Label = "AlpacaExtrasDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            AlpacaExtras.AlpacaExtras.Init(typeof(App).Assembly, "Assets");

            AlpacaExtras.AlpacaExtras.Assets.Add("Ubuntu", new Lazy<byte[]>(() =>
            {
                using (var stream = Assets.Open("Ubuntu-Regular.ttf"))
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    return ms.ToArray();
                }
            }));

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

