using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BW_Launcher.Models;

public class ProcessRunner
{
    public static void RunGame(string id)
    {
        string targetDir = (MainWindowModel.osId == 1) ? "~/.b-world/versions/" : @"C:\Program Files\B-World\versions";
        string targetPath;

        try
        {
            if (targetDir.StartsWith('~'))
            {
                string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                targetDir = Path.Combine(home, targetDir.TrimStart('~', '/', '\\'));
            }

            if (Directory.Exists(targetDir))
            {
                targetPath = Path.Combine(targetDir, (id + ((MainWindowModel.osId == 1) ? ".AppImage" : @"\B-World.exe")));
                /*string[] files = Directory.GetFiles(targetDir);
                LogArray(files);*/

                if (File.Exists(targetPath))
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = targetPath,
                    };

                    Process.Start(startInfo);
                }
                else Log.Information($"{id} not seems to be installed!", "ERROR");
            }
            else
                Log.Information("There is no versions folder!", "ERROR");
        }
        catch (Exception ex)
        {
            Log.Information($"{ex.Message}", "ERROR");
        }
    }
}
