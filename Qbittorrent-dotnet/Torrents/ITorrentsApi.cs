using Qbittorrent_dotnet.DTO.Torrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Torrents
{
    public interface ITorrentsApi
    {
        Task<IList<TorrentInfo>> GetTorrentsAsync(string filter = null, string category = null, string tag = null, string sort = null, bool reverse = false, int? limit = null, int? offset = null, IEnumerable<string> hashes = null, bool? rootPath = null);

        Task<TorrentProperties> GetTorrentPropertiesAsync(string hash);

        Task<IList<TrackerInfo>> GetTorrentTrackersAsync(string hash);

        Task<IList<WebSeedInfo>> GetTorrentWebSeedsAsync(string hash);

        Task<IList<TorrentFile>> GetTorrentFilesAsync(string hash, IEnumerable<int> indexes = null);

        Task<IList<int>> GetTorrentPiecesStatesAsync(string hash);

        Task<IList<string>> GetTorrentPiecesHashesAsync(string hash);

        Task PauseTorrentsAsync(IEnumerable<string> hashes);

        Task ResumeTorrentsAsync(IEnumerable<string> hashes);

        Task DeleteTorrentsAsync(IEnumerable<string> hashes, bool deleteFiles = false);

        Task RecheckTorrentsAsync(IEnumerable<string> hashes);

        Task ReannounceTorrentsAsync(IEnumerable<string> hashes);

        Task EditTrackersAsync(string hash, string toAdd, string toRemove, string toEdit = null);

        Task RemoveTrackersAsync(string hash, IEnumerable<int> trackerIds);

        Task AddPeersAsync(string hash, IEnumerable<string> peers);

        Task AddTorrentByUrlAsync(string urls, string savePath = null, string category = null, bool? autoTMM = null, bool? paused = null, bool? rootFolder = null, string tags = null);

        Task AddTorrentByFilesAsync(IEnumerable<(byte[] Content, string Filename)> torrentFiles, string savePath = null, string category = null, bool? autoTMM = null, bool? paused = null, bool? rootFolder = null, string tags = null);

        Task AddTrackersAsync(string hash, IEnumerable<string> trackers);

        Task IncreasePriorityAsync(IEnumerable<string> hashes);

        Task DecreasePriorityAsync(IEnumerable<string> hashes);

        Task MaximizePriorityAsync(IEnumerable<string> hashes);

        Task MinimizePriorityAsync(IEnumerable<string> hashes);

        Task SetFilePriorityAsync(string hash, int fileIndex, int priority);

        Task<long> GetTorrentDownloadLimitAsync(string hash);

        Task SetTorrentDownloadLimitAsync(IEnumerable<string> hashes, long limit);

        Task SetTorrentShareLimitAsync(IEnumerable<string> hashes, double ratio);

        Task<long> GetTorrentUploadLimitAsync(string hash);

        Task SetTorrentUploadLimitAsync(IEnumerable<string> hashes, long limit);

        Task MoveTorrentStorageAsync(string hash, string location);

        Task RenameTorrentAsync(string hash, string newName);

        Task SetTorrentCategoryAsync(IEnumerable<string> hashes, string category);

        Task<IDictionary<string, CategoryInfo>> GetAllCategoriesAsync();

        Task AddCategoryAsync(string category, string savePath = null);

        Task EditCategoryAsync(string category, string newSavePath = null);

        Task RemoveCategoriesAsync(IEnumerable<string> categories);

        Task<IList<string>> GetAllTagsAsync();

        Task CreateTagsAsync(IEnumerable<string> tags);

        Task DeleteTagsAsync(IEnumerable<string> tags);

        Task AddTagsToTorrentsAsync(IEnumerable<string> hashes, IEnumerable<string> tags);

        Task RemoveTagsFromTorrentsAsync(IEnumerable<string> hashes, IEnumerable<string> tags);

        Task SetAutomaticTorrentManagementAsync(IEnumerable<string> hashes, bool enabled);

        Task ToggleSequentialDownloadAsync(IEnumerable<string> hashes, bool enabled);

        Task SetFirstLastPiecePrioAsync(IEnumerable<string> hashes, bool first, bool last);

        Task SetForceStartAsync(IEnumerable<string> hashes, bool enabled);

        Task SetSuperSeedingAsync(IEnumerable<string> hashes, bool enabled);

        Task RenameFileAsync(string hash, int fileIndex, string newName);

        Task RenameFolderAsync(string hash, string oldFolderPath, string newFolderName);
    }
}