using QBittorrent.Client;
using Qbittorrent_dotnet.DTO.Sync;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Sync
{
    public class SyncApi : ApiClientBase, ISyncApi
    {
        public SyncApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<SyncMainData> GetMainDataAsync(long? rid = null)
        {
            var url = "/api/v2/sync/maindata" + (rid.HasValue ? $"?rid={rid.Value}" : "");
            return await GetJsonAsync<SyncMainData>(url).ConfigureAwait(false);
        }

        public async Task<SyncTorrentPeers> GetTorrentPeersAsync(string hash, long? rid = null)
        {
            var url = $"/api/v2/sync/torrentPeers?hash={hash}" + (rid.HasValue ? $"&rid={rid.Value}" : "");
            return await GetJsonAsync<SyncTorrentPeers>(url).ConfigureAwait(false);
        }
    }
}