using Qbittorrent_dotnet.DTO.App;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.App
{
    public interface IAppApi
    {
        Task<string> GetVersionAsync();

        Task<string> GetWebApiVersionAsync();

        Task<BuildInfo> GetBuildInfoAsync();

        Task<Preferences> GetPreferencesAsync();

        Task SetPreferencesAsync(Preferences preferences);
    }
}