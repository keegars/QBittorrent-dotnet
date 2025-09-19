using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentInfo
    {
        [JsonProperty("dlspeed")] public long DownloadSpeed { get; set; }

        [JsonProperty("eta")] public long ETA { get; set; }

        [JsonProperty("f_l_piece_prio")] public bool FirstLastPiecePrio { get; set; }

        [JsonProperty("force_start")] public bool ForceStart { get; set; }

        [JsonProperty("hash")] public string Hash { get; set; }

        [JsonProperty("category")] public string Category { get; set; }

        [JsonProperty("tags")] public string Tags { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("num_complete")] public int NumComplete { get; set; }

        [JsonProperty("num_incomplete")] public int NumIncomplete { get; set; }

        [JsonProperty("num_leechs")] public int NumLeechs { get; set; }

        [JsonProperty("num_seeds")] public int NumSeeds { get; set; }

        [JsonProperty("priority")] public int Priority { get; set; }

        [JsonProperty("progress")] public double Progress { get; set; }

        [JsonProperty("ratio")] public double Ratio { get; set; }

        [JsonProperty("seq_dl")] public bool SequentialDownload { get; set; }

        [JsonProperty("size")] public long Size { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("super_seeding")] public bool SuperSeeding { get; set; }

        [JsonProperty("upspeed")] public long UploadSpeed { get; set; }

        [JsonProperty("isPrivate")] public bool IsPrivate { get; set; }

        [JsonProperty("save_path")] public string SavePath { get; set; }
    }
}