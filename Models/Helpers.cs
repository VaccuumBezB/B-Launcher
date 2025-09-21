using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
        private static int messageIndex = 0;
        private static readonly object _lock = new();

        public static void Log(string arg, string level = "INFO", [CallerMemberName] string memberName = "")
        {
            lock (_lock)
            {
                var originalFg = Console.ForegroundColor;
                var originalBg = Console.BackgroundColor;

                var levelColor = level switch
                {
                    "ERROR" => ConsoleColor.Red,
                    "WARN"  => ConsoleColor.Yellow,
                    "INFO" => ((messageIndex % 2 == 0) ? ConsoleColor.Blue : ConsoleColor.DarkBlue),
                    "DEBUG" => ConsoleColor.Green,
                    "FUN" => ConsoleColor.Magenta,
                    _ => ConsoleColor.White
                };

                Console.ForegroundColor = levelColor;
                Console.WriteLine($"[B-Launcher] :: ({level}) from [{memberName}] >> " + arg);

                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = originalFg;
                Console.BackgroundColor = originalBg;

                messageIndex++;
            }
        }
    }
}