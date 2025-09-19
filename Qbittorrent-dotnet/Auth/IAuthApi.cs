using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Auth
{
    public interface IAuthApi
    {
        Task LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}