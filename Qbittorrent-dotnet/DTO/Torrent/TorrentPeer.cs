using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentPeer
    {
        [JsonProperty("ip")]
        public string IP { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("progress")]
        public double Progress { get; set; }

        [JsonProperty("flags")]
        public string Flags { get; set; }
    }
}
