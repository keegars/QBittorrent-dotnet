using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class GlobalTransferInfo
    {
        [JsonProperty("dl_info_speed")]
        public long DownloadSpeed { get; set; }

        [JsonProperty("up_info_speed")]
        public long UploadSpeed { get; set; }

        [JsonProperty("dl_info_data")]
        public long Downloaded { get; set; }

        [JsonProperty("up_info_data")]
        public long Uploaded { get; set; }

        [JsonProperty("ratio")]
        public double Ratio { get; set; }

        [JsonProperty("total_queued")]
        public int TotalQueued { get; set; }

        [JsonProperty("total_wanted")]
        public long TotalWanted { get; set; }

        [JsonProperty("total_done")]
        public long TotalDone { get; set; }

        [JsonProperty("total_uploaded")]
        public long TotalUploaded { get; set; }

        [JsonProperty("active_torrents")]
        public int ActiveTorrents { get; set; }

        [JsonProperty("paused_torrents")]
        public int PausedTorrents { get; set; }

        [JsonProperty("queued_torrents")]
        public int QueuedTorrents { get; set; }

        [JsonProperty("alltime_dl")]
        public long AllTimeDownload { get; set; }

        [JsonProperty("alltime_ul")]
        public long AllTimeUpload { get; set; }

        [JsonProperty("connections")]
        public int Connections { get; set; }

        [JsonProperty("dht_nodes")]
        public int DhtNodes { get; set; }

        [JsonProperty("up_limit")]
        public long UploadLimit { get; set; }

        [JsonProperty("dl_limit")]
        public long DownloadLimit { get; set; }
    }
}