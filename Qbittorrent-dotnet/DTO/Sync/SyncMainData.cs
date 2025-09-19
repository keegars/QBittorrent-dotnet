using Newtonsoft.Json;
using Qbittorrent_dotnet.DTO.Torrent;
using System.Collections.Generic;

namespace Qbittorrent_dotnet.DTO.Sync
{
    public class SyncMainData
    {
        [JsonProperty("rid")]
        public long Rid { get; set; }

        [JsonProperty("full_update")]
        public bool FullUpdate { get; set; }

        [JsonProperty("torrents")]
        public Dictionary<string, TorrentInfo> Torrents { get; set; }

        [JsonProperty("categories")]
        public Dictionary<string, object> Categories { get; set; }

        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("server_state")]
        public ServerState ServerState { get; set; }
    }
}