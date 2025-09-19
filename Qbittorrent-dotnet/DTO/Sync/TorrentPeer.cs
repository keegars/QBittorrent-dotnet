using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Sync
{
    public class TorrentPeer
    {
        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("connection")]
        public string Connection { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("dl_speed")]
        public long DlSpeed { get; set; }

        [JsonProperty("up_speed")]
        public long UpSpeed { get; set; }

        [JsonProperty("progress")]
        public double Progress { get; set; }

        [JsonProperty("flags")]
        public string Flags { get; set; }
    }
}