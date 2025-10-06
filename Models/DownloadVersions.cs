using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net.Http;
using Spamton;

namespace BW_Launcher.Models
{
    public class VersionsInstaller
    {
        [BIG_SHOT]
        public static async Task<int> DownloadAsync(string url, string id)
        {
            bool unpackOnLinux = false; // * idk some kind of debug
            string targetDir = (MainWindowModel.osId == 1) ? "~/.b-world/versions/" : @"C:\Program Files\B-World\versions";
            string localFileName = id + ((MainWindowModel.osId == 1) ? ".AppImage" : ".zip");

            try
      {
        if (targetDir.StartsWith("~"))
        {
          string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
          targetDir = Path.Combine(home, targetDir.TrimStart('~', '/', '\\'));
        }

        if (!Directory.Exists(targetDir))
          Directory.CreateDirectory(targetDir);

        string targetPath = Path.Combine(targetDir, localFileName);
        Log.Information($"Target path is {targetPath}");
        Log.Information($"target directory is {targetDir}");

        using var http = new HttpClient();
        using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        resp.EnsureSuccessStatusCode();

        using var stream = await resp.Content.ReadAsStreamAsync();
        using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);

        Task.Run(async () => await stream.CopyToAsync(fs))
            .GetAwaiter().GetResult();
        fs.Dispose();

        Log.Information($"Saved raw file to {targetPath}");

        if (unpackOnLinux || MainWindowModel.osId != 1)
        {
          Log.Information("Unpacking...");
          ZipFile.ExtractToDirectory(targetPath, targetDir);
          Log.Information("Unpacked!");
        }

        return 0;
      }
      catch (UnauthorizedAccessException)
            {
                Log.Error("Access denied. Требуются права для записи в эту папку.");
                return 2;
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return 3;
            }
        }
    }
}