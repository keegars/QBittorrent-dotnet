using QBittorrent.Client;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Tag
{
    public class TagApi : ApiClientBase, ITagApi
    {
        public TagApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<IList<string>> GetTagsAsync()
        {
            return await GetJsonAsync<IList<string>>("/api/v2/torrents/tags").ConfigureAwait(false);
        }

        public async Task AddTagAsync(string tag)
        {
            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tags", tag)
            };

            var resp = await PostFormAsync("/api/v2/torrents/createTags", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task RemoveTagAsync(string tag)
        {
            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tags", tag)
            };

            var resp = await PostFormAsync("/api/v2/torrents/deleteTags", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}