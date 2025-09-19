using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentFile
    {
        [JsonProperty("index")] public int Index { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("size")] public long Size { get; set; }

        [JsonProperty("progress")] public double Progress { get; set; }

        [JsonProperty("priority")] public int Priority { get; set; }

        [JsonProperty("is_seed")] public bool IsSeed { get; set; }

        [JsonProperty("piece_range")] public int[] PieceRange { get; set; }

        [JsonProperty("availability")] public double Availability { get; set; }
    }
}