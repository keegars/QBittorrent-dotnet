using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Category
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("savePath")]
        public string SavePath { get; set; }
    }
}