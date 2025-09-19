using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.App
{
    public class Preferences
    {
        [JsonProperty("save_path")]
        public string SavePath { get; set; }

        [JsonProperty("temp_path")]
        public string TempPath { get; set; }

        [JsonProperty("default_category")]
        public string DefaultCategory { get; set; }

        [JsonProperty("default_save_path")]
        public string DefaultSavePath { get; set; }

        [JsonProperty("auto_tmm_enabled")]
        public bool AutoTmmEnabled { get; set; }

        [JsonProperty("torrent_changed_tmm_enabled")]
        public bool TorrentChangedTmmEnabled { get; set; }

        [JsonProperty("save_path_changed_tmm_enabled")]
        public bool SavePathChangedTmmEnabled { get; set; }

        [JsonProperty("category_changed_tmm_enabled")]
        public bool CategoryChangedTmmEnabled { get; set; }

        [JsonProperty("mail_notification_sender")]
        public string MailNotificationSender { get; set; }

        [JsonProperty("limit_lan_peers")]
        public bool LimitLanPeers { get; set; }

        [JsonProperty("slow_torrent_dl_rate_threshold")]
        public int SlowTorrentDlRateThreshold { get; set; }

        [JsonProperty("slow_torrent_ul_rate_threshold")]
        public int SlowTorrentUlRateThreshold { get; set; }

        [JsonProperty("slow_torrent_inactive_timer")]
        public int SlowTorrentInactiveTimer { get; set; }

        [JsonProperty("alternative_webui_enabled")]
        public bool AlternativeWebuiEnabled { get; set; }

        [JsonProperty("alternative_webui_path")]
        public string AlternativeWebuiPath { get; set; }

        [JsonProperty("piece_extent_affinity")]
        public bool PieceExtentAffinity { get; set; }

        [JsonProperty("web_ui_secure_cookie_enabled")]
        public bool WebUiSecureCookieEnabled { get; set; }

        [JsonProperty("web_ui_max_auth_fail_count")]
        public int WebUiMaxAuthFailCount { get; set; }

        [JsonProperty("web_ui_ban_duration")]
        public int WebUiBanDuration { get; set; }

        [JsonProperty("stop_tracker_timeout")]
        public int StopTrackerTimeout { get; set; }

        [JsonProperty("web_ui_use_custom_http_headers_enabled")]
        public bool WebUiUseCustomHttpHeadersEnabled { get; set; }

        [JsonProperty("web_ui_custom_http_headers")]
        public string WebUiCustomHttpHeaders { get; set; }

        [JsonProperty("rss_download_repack_proper_episodes")]
        public bool RssDownloadRepackProperEpisodes { get; set; }

        [JsonProperty("rss_smart_episode_filters")]
        public bool RssSmartEpisodeFilters { get; set; }

        [JsonProperty("create_subfolder_enabled")]
        public bool CreateSubfolderEnabled { get; set; }

        [JsonProperty("start_paused_enabled")]
        public bool StartPausedEnabled { get; set; }

        [JsonProperty("auto_delete_mode")]
        public int AutoDeleteMode { get; set; }

        [JsonProperty("preallocate_all")]
        public bool PreallocateAll { get; set; }

        [JsonProperty("incomplete_files_ext")]
        public string IncompleteFilesExt { get; set; }
    }
}