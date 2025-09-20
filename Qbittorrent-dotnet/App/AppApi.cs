using QBittorrent.Client;
using Qbittorrent_dotnet.Constants;
using Qbittorrent_dotnet.DTO.App;
using Qbittorrent_dotnet.Helpers;
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

        public async Task<string> GetAppVersionAsync()
        {
            return await GetStringAsync($"{General.AppApiUrl}version").ConfigureAwait(false);
        }

        public async Task<string> GetWebApiVersionAsync()
        {
            return await GetStringAsync($"{General.AppApiUrl}webapiVersion").ConfigureAwait(false);
        }

        public async Task<BuildInfo> GetBuildInfoAsync()
        {
            return await GetJsonAsync<BuildInfo>($"{General.AppApiUrl}buildInfo").ConfigureAwait(false);
        }

        public async Task<Preferences> GetPreferencesAsync()
        {
            return await GetJsonAsync<Preferences>($"{General.AppApiUrl}preferences").ConfigureAwait(false);
        }

        public async Task SetPreferencesAsync(Preferences preferences)
        {
            var form = KeyValuePairHelper.BuildForm(preferences);

            var resp = await PostFormAsync($"{General.AppApiUrl}setPreferences", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
        public async Task RefreshSettingsAsync()
        {
            var response = await PostFormAsync($"{General.AppApiUrl}refreshSettings", null).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}