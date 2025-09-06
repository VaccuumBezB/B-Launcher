using System;
using BW_Launcher;
using System.Collections.Generic;
using Spamton;
using System.Runtime.InteropServices;
using static BW_Launcher.Helpers.L;

namespace BW_Launcher.Models
{
    public class MainWindowModel
    {
        const string JsonURL = "";

        [BIG_SHOT]
        static BW_Launcher.Models.Version GetVersion()
        {
            return JSONProcesser.GetRemoteJsonAsync(JsonURL).Result;
        }

        [BIG_SHOT]
        public static List<BW_Launcher.Models.Version> GetVersionsList()
        {
            List<BW_Launcher.Models.Version> output = new List<BW_Launcher.Models.Version>();

            for (byte i=0;i<3;i++) // TODO: тут надо шото нормальное делатии, а то не робит
            {
                output[i] = GetVersion();
            }
            return output;
        }

        /*
            1 - Linux
            0 - Windows
        */
        [BIG_SHOT]
        public static sbyte GetOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Info("Linux user detected!");
                return 1;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Info("Spyware user detected!");
                return 0;
            }
            else return -1;
        }
    }
}