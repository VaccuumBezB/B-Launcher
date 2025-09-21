using Spamton;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using static BW_Launcher.Helpers.L;

namespace BW_Launcher.Models;

public struct Version
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string linkLinux { get; set; }
    public string linkWindows { get; set; }

    public Version() {this.id="0"; this.name="error"; this.description="ERROR"; this.linkLinux=""; this.linkWindows="";}
}

public class JSONProcesser
{
    public static async Task<List<Version>> GetRemoteJsonAsync(string url)
    {
        string jsonString;

        using HttpClient client = new HttpClient();
        Log($"Http client created for {url}");

        HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
        Log($"Url status {(int)response.StatusCode} {response.ReasonPhrase}");

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();
            Log("Content received", "SUCCESS");
            //Log(jsonString);

            var stopwatch = Stopwatch.StartNew();
                var data = JsonSerializer.Deserialize<List<Version>>(jsonString);
            stopwatch.Stop();
            Log($"Parsing took {stopwatch.ElapsedMilliseconds} ms");

            if (data != null) return data;
            else return new List<Version>();
        }
        else
        {
            Log("Failed to get content from URL", "ERROR");
            return new List<Version>();
        }
    }
}