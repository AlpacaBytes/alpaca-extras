﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AlpacaExtras
{
    public static class AlpacaExtras
    {
        internal static Assembly AssetsAssembly;
        internal static float Scale = 1;

        public static Dictionary<string, Lazy<byte[]>> Assets = new Dictionary<string, Lazy<byte[]>>();

        public static void Init(Assembly assetsAssembly, float scale)
        {
            AssetsAssembly = assetsAssembly;
            Scale = scale;
        }

    }
}
