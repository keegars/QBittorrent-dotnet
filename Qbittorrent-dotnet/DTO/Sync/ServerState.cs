using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Sync
{
    public class ServerState
    {
        [JsonProperty("alltime_dl")]
        public long AllTimeDl { get; set; }

        [JsonProperty("alltime_ul")]
        public long AllTimeUl { get; set; }

        [JsonProperty("dl_info_data")]
        public long DlInfoData { get; set; }

        [JsonProperty("dl_info_speed")]
        public long DlInfoSpeed { get; set; }

        [JsonProperty("up_info_data")]
        public long UpInfoData { get; set; }

        [JsonProperty("up_info_speed")]
        public long UpInfoSpeed { get; set; }

        [JsonProperty("queueing")]
        public bool Queueing { get; set; }

        [JsonProperty("use_alt_speed_limits")]
        public bool UseAltSpeedLimits { get; set; }
    }
}