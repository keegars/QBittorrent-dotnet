using Newtonsoft.Json;
using System.Collections.Generic;

namespace Qbittorrent_dotnet.DTO.Sync
{
    public class SyncTorrentPeers
    {
        [JsonProperty("rid")]
        public long Rid { get; set; }

        [JsonProperty("full_update")]
        public bool FullUpdate { get; set; }

        [JsonProperty("peers")]
        public Dictionary<string, TorrentPeer> Peers { get; set; }
    }
}