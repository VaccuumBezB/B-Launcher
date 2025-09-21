using System;
using BW_Launcher;
using System.Collections.Generic;
using Spamton;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using static BW_Launcher.Helpers.L;

namespace BW_Launcher.Models
{
    public class MainWindowModel
    {
        const string JsonURL = "http://localhost:8080/versions.json"; // TODO ПОДНЯТЬ СЕРВАК
        public static sbyte osId { get; } = GetOS();

        /*[BIG_SHOT]
        static BW_Launcher.Models.Version GetVersion()
        {
            return JSONProcesser.GetRemoteJsonAsync(JsonURL).Result;
        }*/

        public static List<BW_Launcher.Models.Version> versionsList = GetVersionsList();

        [BIG_SHOT]
        public static List<BW_Launcher.Models.Version> GetVersionsList()
        {
            List<BW_Launcher.Models.Version> output = JSONProcesser.GetRemoteJsonAsync(JsonURL).Result;

            ////for (byte i=0;i<versionsAmount;i++) тут надо шото нормальное делатии, а то не робит
            ////{
            ////    output[i] = GetVersion();
            ////}

            if (output == null)
            {
                Log("Versions list is null", "ERROR");
                return new List<BW_Launcher.Models.Version>();
            }
            else
                Log($"versions count: {output.Count}", "DEBUG");
                return output;
        }

        [BIG_SHOT]
        public static ObservableCollection<string> GetVersionsDisplayNames()
        {
            ObservableCollection<string> output = new ObservableCollection<string>();

            for (byte i=0;i<versionsList.Count;i++)
            {
                output.Add(versionsList[i].name);
            }
            
            Log($"Latest version is {output[0]}", "FUN");

            return output;
            //return new ObservableCollection<string>() {"DEBUG"};
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
                Log("Linux user detected!");
                return 1;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Log("Microsoft Spyware user detected!");
                return 0;
            }
            else return -1;
        }
    }
}