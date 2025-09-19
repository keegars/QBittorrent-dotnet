using QBittorrent.Client;
using Qbittorrent_dotnet.DTO.Torrent;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.GlobalTransfer
{
    public class GlobalTransferApi : ApiClientBase, IGlobalTransferApi
    {
        public GlobalTransferApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<GlobalTransferInfo> GetGlobalTransferInfoAsync()
        {
            return await GetJsonAsync<GlobalTransferInfo>("/api/v2/transfer/info").ConfigureAwait(false);
        }
    }
}