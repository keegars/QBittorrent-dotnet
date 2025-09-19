using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace QBittorrent.Client
{
    public abstract class ApiClientBase
    {
        protected readonly HttpClient Http;
        protected readonly string BaseUrl;
        protected readonly CookieContainer CookieContainer;

        protected ApiClientBase(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
        {
            Http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            BaseUrl = (baseUrl ?? throw new ArgumentNullException(nameof(baseUrl))).TrimEnd('/');
            CookieContainer = cookieContainer;
        }

        protected async Task<string> GetStringAsync(string relativePath)
        {
            var resp = await Http.GetAsync(BaseUrl + relativePath).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        protected async Task<T> GetJsonAsync<T>(string relativePath)
        {
            var s = await GetStringAsync(relativePath).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(s);
        }

        protected async Task<HttpResponseMessage> PostFormAsync(string relativePath, IEnumerable<KeyValuePair<string, string>> formData)
        {
            var content = new FormUrlEncodedContent(formData ?? new List<KeyValuePair<string, string>>());
            return await Http.PostAsync(BaseUrl + relativePath, content).ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> PostEmptyAsync(string relativePath)
        {
            return await Http.PostAsync(BaseUrl + relativePath, null).ConfigureAwait(false);
        }
    }
}