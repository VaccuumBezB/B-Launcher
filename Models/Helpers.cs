using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace BW_Launcher.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }
    }

    public static class L
    {
        public static void Info(string arg)
        {
            Console.WriteLine("[B-Launcher] :: (INFO) >> " + arg);
        }
    }
}