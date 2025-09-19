using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentContent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("progress")]
        public double Progress { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("is_seed")]
        public bool IsSeed { get; set; }
    }
}