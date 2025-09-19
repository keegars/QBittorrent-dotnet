using QBittorrent.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Auth
{
    public class AuthApi : ApiClientBase, IAuthApi
    {
        public AuthApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
        : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var form = new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            var resp = await PostFormAsync("/api/v2/auth/login", form).ConfigureAwait(false);

            if (resp.StatusCode == HttpStatusCode.Forbidden)
                throw new InvalidOperationException("Login forbidden: IP may be banned due to failed attempts.");

            resp.EnsureSuccessStatusCode();
            // on success server returns Set-Cookie: SID=...; CookieContainer will capture it
        }

        public async Task LogoutAsync()
        {
            var resp = await PostEmptyAsync("/api/v2/auth/logout").ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}