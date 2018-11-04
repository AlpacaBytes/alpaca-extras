using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms.Internals;

namespace AlpacaExtras
{
    [Preserve(AllMembers = true)]
    public static class AlpacaExtras
    {
        internal static Assembly AssetsAssembly;
        internal static string AssetFolder;

        public static Dictionary<string, Lazy<byte[]>> Assets = new Dictionary<string, Lazy<byte[]>>();

        public static void Init(Assembly assetsAssembly, string assetFolder = null)
        {
            AssetsAssembly = assetsAssembly;
            AssetFolder = assetFolder;
        }

    }
}
