using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net.Http;
using Spamton;
using BW_Launcher.Models;
using static BW_Launcher.Helpers.L;

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
                Log($"Target path is {targetPath}", "INFO");
                Log($"target directory is {targetDir}");

                using var http = new HttpClient();
                using var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                resp.EnsureSuccessStatusCode();

                using var stream = await resp.Content.ReadAsStreamAsync();
                using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await stream.CopyToAsync(fs);
                fs.Dispose();

                Log($"Saved raw file to {targetPath}");
                
                if (unpackOnLinux || MainWindowModel.osId != 1)
                {
                    Log("Распаковка...");
                    ZipFile.ExtractToDirectory(targetPath, targetDir);
                    Log("Распаковано!");
                }

                return 0;
            }
            catch (UnauthorizedAccessException)
            {
                Log("Access denied. Требуются права для записи в эту папку.", "ERROR");
                return 2;
            }
            catch (Exception ex)
            {
                Log($"{ex.Message}", "ERROR");
                return 3;
            }
        }
    }
}