using Spamton;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BW_Launcher.Models;

public struct Version
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class JSONProcesser
{
    public static async Task<Version> GetRemoteJsonAsync(string url)
    {
        using HttpClient client = new HttpClient();
        var jsonString = await client.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<Version>(jsonString);
        return data;
    }
}