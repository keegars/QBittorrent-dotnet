using Qbittorrent_dotnet.DTO.Torrent;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.GlobalTransfer
{
    public interface IGlobalTransferApi
    {
        /// <summary> Gets global transfer info, including speeds, totals, queue counts, and connections. </summary>
        Task<GlobalTransferInfo> GetGlobalTransferInfoAsync();
    }
}