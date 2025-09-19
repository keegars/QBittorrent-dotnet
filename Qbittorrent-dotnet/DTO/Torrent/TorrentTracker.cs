using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentTracker
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tier")]
        public int Tier { get; set; }

        [JsonProperty("numPeers")]
        public int NumPeers { get; set; }
    }
}
