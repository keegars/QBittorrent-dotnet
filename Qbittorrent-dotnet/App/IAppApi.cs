using Qbittorrent_dotnet.DTO.App;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.App
{
    /// <summary>
    /// Provides information and preferences for the qBittorrent application.
    /// </summary>
    public interface IAppApi
    {
        /// <summary>Gets the qBittorrent version.</summary>
        /// <returns>Version string (e.g. "4.3.3")</returns>
        Task<string> GetAppVersionAsync();

        /// <summary>Gets the Web API version.</summary>
        /// <returns>Web API version string (e.g. "2.0")</returns>
        Task<string> GetWebApiVersionAsync();

        /// <summary>Gets build information.</summary>
        /// <returns>BuildInfo object containing build details.</returns>
        Task<BuildInfo> GetBuildInfoAsync();

        /// <summary>
        /// Shuts down the qBittorrent application.
        /// </summary>
        Task ShutdownAsync();

        /// <summary>Retrieves all application preferences.</summary>
        /// <returns>Preferences object containing current settings.</returns>
        Task<Preferences> GetPreferencesAsync();

        /// <summary>Sets application preferences.</summary>
        Task SetPreferencesAsync(Preferences prefs);

        /// <summary>Refreshes the application session settings.</summary>
        Task RefreshSettingsAsync();
    }
}