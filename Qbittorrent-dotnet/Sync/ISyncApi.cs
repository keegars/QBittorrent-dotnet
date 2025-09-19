using Qbittorrent_dotnet.DTO.Sync;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Sync
{
    public interface ISyncApi
    {
        Task<SyncMainData> GetMainDataAsync(long? rid = null);

        Task<SyncTorrentPeers> GetTorrentPeersAsync(string hash, long? rid = null);
    }
}