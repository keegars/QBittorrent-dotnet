using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class CategoryInfo
    {
        [JsonProperty("savePath")] public string SavePath { get; set; }
    }
}