using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.Torrent
{
    public class TorrentProperties
    {
        [JsonProperty("save_path")] public string SavePath { get; set; }

        [JsonProperty("creation_date")] public long CreationDate { get; set; }

        [JsonProperty("piece_size")] public int PieceSize { get; set; }

        [JsonProperty("comment")] public string Comment { get; set; }

        [JsonProperty("total_wasted")] public long TotalWasted { get; set; }

        [JsonProperty("total_uploaded")] public long TotalUploaded { get; set; }

        [JsonProperty("total_uploaded_session")] public long TotalUploadedSession { get; set; }

        [JsonProperty("total_downloaded")] public long TotalDownloaded { get; set; }

        [JsonProperty("total_downloaded_session")] public long TotalDownloadedSession { get; set; }

        [JsonProperty("up_limit")] public long UpLimit { get; set; }

        [JsonProperty("dl_limit")] public long DlLimit { get; set; }

        [JsonProperty("time_elapsed")] public long TimeElapsed { get; set; }

        [JsonProperty("seeding_time")] public long SeedingTime { get; set; }

        [JsonProperty("nb_connections")] public int NbConnections { get; set; }

        [JsonProperty("nb_connections_limit")] public int NbConnectionsLimit { get; set; }

        [JsonProperty("share_ratio")] public double ShareRatio { get; set; }

        [JsonProperty("addition_date")] public long AdditionDate { get; set; }

        [JsonProperty("completion_date")] public long CompletionDate { get; set; }

        [JsonProperty("created_by")] public string CreatedBy { get; set; }

        [JsonProperty("dl_speed_avg")] public long DlSpeedAvg { get; set; }

        [JsonProperty("dl_speed")] public long DlSpeed { get; set; }

        [JsonProperty("eta")] public long ETA { get; set; }

        [JsonProperty("last_seen")] public long LastSeen { get; set; }

        [JsonProperty("peers")] public int Peers { get; set; }

        [JsonProperty("peers_total")] public int PeersTotal { get; set; }

        [JsonProperty("pieces_have")] public int PiecesHave { get; set; }

        [JsonProperty("pieces_num")] public int PiecesNum { get; set; }

        [JsonProperty("reannounce")] public int Reannounce { get; set; }

        [JsonProperty("seeds")] public int Seeds { get; set; }

        [JsonProperty("seeds_total")] public int SeedsTotal { get; set; }

        [JsonProperty("total_size")] public long TotalSize { get; set; }

        [JsonProperty("up_speed_avg")] public long UpSpeedAvg { get; set; }

        [JsonProperty("up_speed")] public long UpSpeed { get; set; }

        [JsonProperty("isPrivate")] public bool IsPrivate { get; set; }
    }
}