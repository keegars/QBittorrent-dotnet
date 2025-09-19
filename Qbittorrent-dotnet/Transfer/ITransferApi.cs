using Qbittorrent_dotnet.DTO.Torrent;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Transfer
{
    public interface ITransferApi
    {
        Task<GlobalTransferInfo> GetGlobalTransferInfoAsync();
    }
}