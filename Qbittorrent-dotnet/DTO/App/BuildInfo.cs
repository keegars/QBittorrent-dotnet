using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.App
{
    public class BuildInfo
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("gitCommit")]
        public string GitCommit { get; set; }

        [JsonProperty("buildDate")]
        public string BuildDate { get; set; }

        [JsonProperty("qtVersion")]
        public string QtVersion { get; set; }

        [JsonProperty("libtorrentVersion")]
        public string LibtorrentVersion { get; set; }

        [JsonProperty("boostVersion")]
        public string BoostVersion { get; set; }
    }
}