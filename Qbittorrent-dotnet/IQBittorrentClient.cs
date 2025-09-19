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
using System.Threading.Tasks;

namespace Qbittorrent_dotnet
{
    public interface IQBittorrentClient : IDisposable
    {
        IAuthApi Auth { get; }
        IAppApi App { get; }
        ITransferApi Transfer { get; }
        ITorrentsApi Torrents { get; }
        ILogApi Log { get; }
        ISyncApi Sync { get; }
        IGlobalTransferApi GlobalTransfer { get; }
        ICategoryApi Categories { get; }
        ITagApi Tags { get; }

        Task LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}