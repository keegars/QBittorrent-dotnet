using Qbittorrent_dotnet.App;
using Qbittorrent_dotnet.Auth;
using Qbittorrent_dotnet.Category;
using Qbittorrent_dotnet.GlobalTransfer;
using Qbittorrent_dotnet.Log;
using Qbittorrent_dotnet.Sync;
using Qbittorrent_dotnet.Tag;
using Qbittorrent_dotnet.Torrents;
using Qbittorrent_dotnet.Transfer;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet
{
    public class QBittorrentClient : IQBittorrentClient
    {
        private readonly HttpClient _http;
        private readonly HttpClientHandler _handler;
        private readonly CookieContainer _cookieContainer;
        private bool _disposed;

        public QBittorrentClient(string baseUrl, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));

            _cookieContainer = new CookieContainer();
            _handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _http = new HttpClient(_handler);
            if (timeout.HasValue) _http.Timeout = timeout.Value;

            // Per API docs, Referer or Origin should be set to the same domain as Host header
            try { _http.DefaultRequestHeaders.Add("Referer", baseUrl.TrimEnd('/')); } catch { }

            // Initialize APIs
            Auth = new AuthApi(_http, baseUrl, _cookieContainer);
            App = new AppApi(_http, baseUrl, _cookieContainer);
            Transfer = new TransferApi(_http, baseUrl, _cookieContainer);
            Torrents = new TorrentsApi(_http, baseUrl, _cookieContainer);
            Log = new LogApi(_http, baseUrl, _cookieContainer);
            Sync = new SyncApi(_http, baseUrl, _cookieContainer);
            App = new AppApi(_http, baseUrl, _cookieContainer);
            GlobalTransfer = new GlobalTransferApi(_http, baseUrl, _cookieContainer);
            Categories = new CategoryApi(_http, baseUrl, _cookieContainer);
            Tags = new TagApi(_http, baseUrl, _cookieContainer);
        }

        public IAuthApi Auth { get; }

        public IAppApi App { get; }

        public ITransferApi Transfer { get; }

        public ITorrentsApi Torrents { get; }
        public ILogApi Log { get; }
        public ISyncApi Sync { get; }
        public IGlobalTransferApi GlobalTransfer { get; }
        public ICategoryApi Categories { get; }
        public ITagApi Tags { get; }

        public Task LoginAsync(string username, string password) => Auth.LoginAsync(username, password);

        public Task LogoutAsync() => Auth.LogoutAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // attempt to logout synchronously to avoid leaving session behind
                try
                {
                    Auth.LogoutAsync().GetAwaiter().GetResult();
                }
                catch
                {
                    // swallow - Dispose should not throw
                }

                try { _http.Dispose(); } catch { }
                try { _handler.Dispose(); } catch { }
            }

            _disposed = true;
        }
    }
}