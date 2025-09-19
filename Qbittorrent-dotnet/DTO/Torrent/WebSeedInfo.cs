using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class WebSeedInfo
    { [JsonProperty("url")] public string Url { get; set; } }
}