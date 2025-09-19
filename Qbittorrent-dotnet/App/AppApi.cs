using QBittorrent.Client;
using Qbittorrent_dotnet.DTO.App;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.App
{
    public class AppApi : ApiClientBase, IAppApi
    {
        public AppApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<string> GetVersionAsync()
        {
            return await GetStringAsync("/api/v2/app/version").ConfigureAwait(false);
        }

        public async Task<string> GetWebApiVersionAsync()
        {
            return await GetStringAsync("/api/v2/app/webapiVersion").ConfigureAwait(false);
        }

        public async Task<BuildInfo> GetBuildInfoAsync()
        {
            return await GetJsonAsync<BuildInfo>("/api/v2/app/buildInfo").ConfigureAwait(false);
        }

        public async Task<Preferences> GetPreferencesAsync()
        {
            return await GetJsonAsync<Preferences>("/api/v2/app/preferences").ConfigureAwait(false);
        }

        public async Task SetPreferencesAsync(Preferences preferences)
        {
            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("save_path", preferences.SavePath ?? ""),
                new KeyValuePair<string, string>("temp_path", preferences.TempPath ?? ""),
                new KeyValuePair<string, string>("default_category", preferences.DefaultCategory ?? ""),
                new KeyValuePair<string, string>("default_save_path", preferences.DefaultSavePath ?? ""),
                new KeyValuePair<string, string>("auto_tmm_enabled", preferences.AutoTmmEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("torrent_changed_tmm_enabled", preferences.TorrentChangedTmmEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("save_path_changed_tmm_enabled", preferences.SavePathChangedTmmEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("category_changed_tmm_enabled", preferences.CategoryChangedTmmEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("mail_notification_sender", preferences.MailNotificationSender ?? ""),
                new KeyValuePair<string, string>("limit_lan_peers", preferences.LimitLanPeers.ToString().ToLower()),
                new KeyValuePair<string, string>("slow_torrent_dl_rate_threshold", preferences.SlowTorrentDlRateThreshold.ToString()),
                new KeyValuePair<string, string>("slow_torrent_ul_rate_threshold", preferences.SlowTorrentUlRateThreshold.ToString()),
                new KeyValuePair<string, string>("slow_torrent_inactive_timer", preferences.SlowTorrentInactiveTimer.ToString()),
                new KeyValuePair<string, string>("alternative_webui_enabled", preferences.AlternativeWebuiEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("alternative_webui_path", preferences.AlternativeWebuiPath ?? ""),
                new KeyValuePair<string, string>("piece_extent_affinity", preferences.PieceExtentAffinity.ToString().ToLower()),
                new KeyValuePair<string, string>("web_ui_secure_cookie_enabled", preferences.WebUiSecureCookieEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("web_ui_max_auth_fail_count", preferences.WebUiMaxAuthFailCount.ToString()),
                new KeyValuePair<string, string>("web_ui_ban_duration", preferences.WebUiBanDuration.ToString()),
                new KeyValuePair<string, string>("stop_tracker_timeout", preferences.StopTrackerTimeout.ToString()),
                new KeyValuePair<string, string>("web_ui_use_custom_http_headers_enabled", preferences.WebUiUseCustomHttpHeadersEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("web_ui_custom_http_headers", preferences.WebUiCustomHttpHeaders ?? ""),
                new KeyValuePair<string, string>("rss_download_repack_proper_episodes", preferences.RssDownloadRepackProperEpisodes.ToString().ToLower()),
                new KeyValuePair<string, string>("rss_smart_episode_filters", preferences.RssSmartEpisodeFilters.ToString().ToLower()),
                new KeyValuePair<string, string>("create_subfolder_enabled", preferences.CreateSubfolderEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("start_paused_enabled", preferences.StartPausedEnabled.ToString().ToLower()),
                new KeyValuePair<string, string>("auto_delete_mode", preferences.AutoDeleteMode.ToString()),
                new KeyValuePair<string, string>("preallocate_all", preferences.PreallocateAll.ToString().ToLower()),
                new KeyValuePair<string, string>("incomplete_files_ext", preferences.IncompleteFilesExt ?? "")
            };

            var resp = await PostFormAsync("/api/v2/app/setPreferences", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}