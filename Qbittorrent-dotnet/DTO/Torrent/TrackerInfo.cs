using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TrackerInfo
    {
        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("status")] public int Status { get; set; }

        [JsonProperty("tier")] public int Tier { get; set; }

        [JsonProperty("num_peers")] public int NumPeers { get; set; }

        [JsonProperty("num_seeds")] public int NumSeeds { get; set; }

        [JsonProperty("num_leeches")] public int NumLeeches { get; set; }

        [JsonProperty("num_downloaded")] public int NumDownloaded { get; set; }

        [JsonProperty("msg")] public string Message { get; set; }
    }
}