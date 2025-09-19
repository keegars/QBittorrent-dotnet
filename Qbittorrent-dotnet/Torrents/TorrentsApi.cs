using QBittorrent.Client;
using Qbittorrent_dotnet.DTO.Torrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Torrents
{
    public class TorrentsApi : ApiClientBase, ITorrentsApi
    {
        public TorrentsApi(HttpClient httpClient, string baseUrl, System.Net.CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<IList<TorrentInfo>> GetTorrentsAsync(string filter = null, string category = null, string tag = null, string sort = null, bool reverse = false, int? limit = null, int? offset = null, IEnumerable<string> hashes = null, bool? rootPath = null)
        {
            var qs = new List<string>();
            void Add(string k, string v) { if (!string.IsNullOrEmpty(v)) qs.Add($"{k}={Uri.EscapeDataString(v)}"); }
            Add("filter", filter);
            Add("category", category);
            Add("tag", tag);
            Add("sort", sort);
            if (reverse) Add("reverse", "true");
            if (limit.HasValue) Add("limit", limit.Value.ToString());
            if (offset.HasValue) Add("offset", offset.Value.ToString());
            var h = JoinHashes(hashes);
            Add("hashes", h);
            if (rootPath.HasValue) Add("root_path", rootPath.Value ? "true" : "false");

            var path = "/api/v2/torrents/info" + (qs.Count > 0 ? "?" + string.Join("&", qs) : string.Empty);
            return await GetJsonAsync<List<TorrentInfo>>(path).ConfigureAwait(false);
        }

        public async Task<TorrentProperties> GetTorrentPropertiesAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/properties?hash={Uri.EscapeDataString(hash)}";
            return await GetJsonAsync<TorrentProperties>(path).ConfigureAwait(false);
        }

        public async Task<IList<TrackerInfo>> GetTorrentTrackersAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/trackers?hash={Uri.EscapeDataString(hash)}";
            return await GetJsonAsync<List<TrackerInfo>>(path).ConfigureAwait(false);
        }

        public async Task<IList<WebSeedInfo>> GetTorrentWebSeedsAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/webseeds?hash={Uri.EscapeDataString(hash)}";
            return await GetJsonAsync<List<WebSeedInfo>>(path).ConfigureAwait(false);
        }

        public async Task<IList<TorrentFile>> GetTorrentFilesAsync(string hash, IEnumerable<int> indexes = null)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var qs = $"hash={Uri.EscapeDataString(hash)}";
            var idx = JoinIndexes(indexes);
            if (!string.IsNullOrEmpty(idx)) qs += $"&indexes={Uri.EscapeDataString(idx)}";
            var path = $"/api/v2/torrents/files?{qs}";
            return await GetJsonAsync<List<TorrentFile>>(path).ConfigureAwait(false);
        }

        public async Task<IList<int>> GetTorrentPiecesStatesAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/pieceStates?hash={Uri.EscapeDataString(hash)}";
            return await GetJsonAsync<List<int>>(path).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetTorrentPiecesHashesAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/pieceHashes?hash={Uri.EscapeDataString(hash)}";
            return await GetJsonAsync<List<string>>(path).ConfigureAwait(false);
        }

        public async Task PauseTorrentsAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/pause", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task ResumeTorrentsAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/resume", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task DeleteTorrentsAsync(IEnumerable<string> hashes, bool deleteFiles = false)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            list.Add(new KeyValuePair<string, string>("deleteFiles", deleteFiles ? "true" : "false"));
            var resp = await PostFormAsync("/api/v2/torrents/delete", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RecheckTorrentsAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/recheck", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task ReannounceTorrentsAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/reannounce", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task EditTrackersAsync(string hash, string toAdd, string toRemove, string toEdit = null)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var form = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hash", hash) };
            if (!string.IsNullOrEmpty(toAdd)) form.Add(new KeyValuePair<string, string>("trackers", toAdd));
            if (!string.IsNullOrEmpty(toRemove)) form.Add(new KeyValuePair<string, string>("to_remove", toRemove));
            if (!string.IsNullOrEmpty(toEdit)) form.Add(new KeyValuePair<string, string>("to_edit", toEdit));
            var resp = await PostFormAsync("/api/v2/torrents/editTracker", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RemoveTrackersAsync(string hash, IEnumerable<int> trackerIds)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var ids = trackerIds == null ? string.Empty : string.Join("|", trackerIds);
            var form = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("ids", ids) };
            var resp = await PostFormAsync("/api/v2/torrents/removeTrackers", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task AddPeersAsync(string hash, IEnumerable<string> peers)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (peers == null) throw new ArgumentNullException(nameof(peers));
            var p = string.Join("|", peers);
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("peers", p) };
            var resp = await PostFormAsync("/api/v2/torrents/addPeers", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task AddTorrentByUrlAsync(string urls, string savePath = null, string category = null, bool? autoTMM = null, bool? paused = null, bool? rootFolder = null, string tags = null)
        {
            if (string.IsNullOrEmpty(urls)) throw new ArgumentNullException(nameof(urls));
            var form = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("urls", urls) };
            if (!string.IsNullOrEmpty(savePath)) form.Add(new KeyValuePair<string, string>("savepath", savePath));
            if (!string.IsNullOrEmpty(category)) form.Add(new KeyValuePair<string, string>("category", category));
            if (autoTMM.HasValue) form.Add(new KeyValuePair<string, string>("autoTMM", autoTMM.Value ? "true" : "false"));
            if (paused.HasValue) form.Add(new KeyValuePair<string, string>("paused", paused.Value ? "true" : "false"));
            if (rootFolder.HasValue) form.Add(new KeyValuePair<string, string>("root_folder", rootFolder.Value ? "true" : "false"));
            if (!string.IsNullOrEmpty(tags)) form.Add(new KeyValuePair<string, string>("tags", tags));
            var resp = await PostFormAsync("/api/v2/torrents/add", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task AddTorrentByFilesAsync(IEnumerable<(byte[] Content, string Filename)> torrentFiles, string savePath = null, string category = null, bool? autoTMM = null, bool? paused = null, bool? rootFolder = null, string tags = null)
        {
            if (torrentFiles == null) throw new ArgumentNullException(nameof(torrentFiles));
            var list = torrentFiles as (byte[] Content, string Filename)[] ?? torrentFiles.ToArray();
            if (!list.Any()) throw new ArgumentException("No files provided", nameof(torrentFiles));

            using (var content = new MultipartFormDataContent())
            {
                foreach (var file in list)
                {
                    var b = new ByteArrayContent(file.Content);
                    content.Add(b, "torrents", file.Filename);
                }

                if (!string.IsNullOrEmpty(savePath)) content.Add(new StringContent(savePath), "savepath");
                if (!string.IsNullOrEmpty(category)) content.Add(new StringContent(category), "category");
                if (autoTMM.HasValue) content.Add(new StringContent(autoTMM.Value ? "true" : "false"), "autoTMM");
                if (paused.HasValue) content.Add(new StringContent(paused.Value ? "true" : "false"), "paused");
                if (rootFolder.HasValue) content.Add(new StringContent(rootFolder.Value ? "true" : "false"), "root_folder");
                if (!string.IsNullOrEmpty(tags)) content.Add(new StringContent(tags), "tags");

                var resp = await Http.PostAsync(BaseUrl + "/api/v2/torrents/add", content).ConfigureAwait(false);
                await EnsureSuccess(resp).ConfigureAwait(false);
            }
        }

        public async Task AddTrackersAsync(string hash, IEnumerable<string> trackers)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (trackers == null) throw new ArgumentNullException(nameof(trackers));
            var trackersJoined = string.Join("", trackers);
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("trackers", trackersJoined) };
            var resp = await PostFormAsync("/api/v2/torrents/addTrackers", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task IncreasePriorityAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/increasePrio", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task DecreasePriorityAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/decreasePrio", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task MaximizePriorityAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/topPrio", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task MinimizePriorityAsync(IEnumerable<string> hashes)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)) };
            var resp = await PostFormAsync("/api/v2/torrents/bottomPrio", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetFilePriorityAsync(string hash, int fileIndex, int priority)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("id", fileIndex.ToString()), new KeyValuePair<string, string>("priority", priority.ToString()) };
            var resp = await PostFormAsync("/api/v2/torrents/filePrio", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task<long> GetTorrentDownloadLimitAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/downloadLimit?hash={Uri.EscapeDataString(hash)}";
            var s = await GetStringAsync(path).ConfigureAwait(false);
            if (long.TryParse(s, out var v)) return v;
            throw new InvalidOperationException("Unexpected response for download limit");
        }

        public async Task SetTorrentDownloadLimitAsync(IEnumerable<string> hashes, long limit)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("limit", limit.ToString()) };
            var resp = await PostFormAsync("/api/v2/torrents/setDownloadLimit", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetTorrentShareLimitAsync(IEnumerable<string> hashes, double ratio)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("limit", ratio.ToString(System.Globalization.CultureInfo.InvariantCulture)) };
            var resp = await PostFormAsync("/api/v2/torrents/setShareLimits", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task<long> GetTorrentUploadLimitAsync(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            var path = $"/api/v2/torrents/uploadLimit?hash={Uri.EscapeDataString(hash)}";
            var s = await GetStringAsync(path).ConfigureAwait(false);
            if (long.TryParse(s, out var v)) return v;
            throw new InvalidOperationException("Unexpected response for upload limit");
        }

        public async Task SetTorrentUploadLimitAsync(IEnumerable<string> hashes, long limit)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("limit", limit.ToString()) };
            var resp = await PostFormAsync("/api/v2/torrents/setUploadLimit", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task MoveTorrentStorageAsync(string hash, string location)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (string.IsNullOrEmpty(location)) throw new ArgumentNullException(nameof(location));
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", hash), new KeyValuePair<string, string>("location", location) };
            var resp = await PostFormAsync("/api/v2/torrents/setLocation", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RenameTorrentAsync(string hash, string newName)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (string.IsNullOrEmpty(newName)) throw new ArgumentNullException(nameof(newName));
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("name", newName) };
            var resp = await PostFormAsync("/api/v2/torrents/rename", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetTorrentCategoryAsync(IEnumerable<string> hashes, string category)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("category", category ?? string.Empty) };
            var resp = await PostFormAsync("/api/v2/torrents/setCategory", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task<IDictionary<string, CategoryInfo>> GetAllCategoriesAsync()
        {
            var path = "/api/v2/torrents/categories";
            return await GetJsonAsync<Dictionary<string, CategoryInfo>>(path).ConfigureAwait(false);
        }

        public async Task AddCategoryAsync(string category, string savePath = null)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("category", category) };
            if (!string.IsNullOrEmpty(savePath)) list.Add(new KeyValuePair<string, string>("savepath", savePath));
            var resp = await PostFormAsync("/api/v2/torrents/addCategory", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task EditCategoryAsync(string category, string newSavePath = null)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException(nameof(category));
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("category", category) };
            if (!string.IsNullOrEmpty(newSavePath)) list.Add(new KeyValuePair<string, string>("savepath", newSavePath));
            var resp = await PostFormAsync("/api/v2/torrents/editCategory", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RemoveCategoriesAsync(IEnumerable<string> categories)
        {
            if (categories == null) throw new ArgumentNullException(nameof(categories));
            var cats = string.Join("", categories);
            var form = new[] { new KeyValuePair<string, string>("categories", cats) };
            var resp = await PostFormAsync("/api/v2/torrents/removeCategories", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetAllTagsAsync()
        {
            var path = "/api/v2/torrents/tags";
            return await GetJsonAsync<List<string>>(path).ConfigureAwait(false);
        }

        public async Task CreateTagsAsync(IEnumerable<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            var form = new[] { new KeyValuePair<string, string>("tags", string.Join(",", tags)) };
            var resp = await PostFormAsync("/api/v2/torrents/createTags", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task DeleteTagsAsync(IEnumerable<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            var form = new[] { new KeyValuePair<string, string>("tags", string.Join(",", tags)) };
            var resp = await PostFormAsync("/api/v2/torrents/deleteTags", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task AddTagsToTorrentsAsync(IEnumerable<string> hashes, IEnumerable<string> tags)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("tags", string.Join(",", tags ?? Enumerable.Empty<string>())) };
            var resp = await PostFormAsync("/api/v2/torrents/addTags", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RemoveTagsFromTorrentsAsync(IEnumerable<string> hashes, IEnumerable<string> tags)
        {
            var form = new[] { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("tags", string.Join(",", tags ?? Enumerable.Empty<string>())) };
            var resp = await PostFormAsync("/api/v2/torrents/removeTags", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetAutomaticTorrentManagementAsync(IEnumerable<string> hashes, bool enabled)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("enable", enabled ? "true" : "false") };
            var resp = await PostFormAsync("/api/v2/torrents/setAutoManagement", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task ToggleSequentialDownloadAsync(IEnumerable<string> hashes, bool enabled)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("enable", enabled ? "true" : "false") };
            var resp = await PostFormAsync("/api/v2/torrents/setSequentialDownload", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetFirstLastPiecePrioAsync(IEnumerable<string> hashes, bool first, bool last)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("first", first ? "true" : "false"), new KeyValuePair<string, string>("last", last ? "true" : "false") };
            var resp = await PostFormAsync("/api/v2/torrents/setFirstLastPiecePrio", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetForceStartAsync(IEnumerable<string> hashes, bool enabled)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("enable", enabled ? "true" : "false") };
            var resp = await PostFormAsync("/api/v2/torrents/setForceStart", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task SetSuperSeedingAsync(IEnumerable<string> hashes, bool enabled)
        {
            var list = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("hashes", JoinHashes(hashes)), new KeyValuePair<string, string>("enable", enabled ? "true" : "false") };
            var resp = await PostFormAsync("/api/v2/torrents/setSuperSeeding", list).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RenameFileAsync(string hash, int fileIndex, string newName)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (string.IsNullOrEmpty(newName)) throw new ArgumentNullException(nameof(newName));
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("id", fileIndex.ToString()), new KeyValuePair<string, string>("name", newName) };
            var resp = await PostFormAsync("/api/v2/torrents/renameFile", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task RenameFolderAsync(string hash, string oldFolderPath, string newFolderName)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash));
            if (string.IsNullOrEmpty(oldFolderPath)) throw new ArgumentNullException(nameof(oldFolderPath));
            if (string.IsNullOrEmpty(newFolderName)) throw new ArgumentNullException(nameof(newFolderName));
            var form = new[] { new KeyValuePair<string, string>("hash", hash), new KeyValuePair<string, string>("path", oldFolderPath), new KeyValuePair<string, string>("name", newFolderName) };
            var resp = await PostFormAsync("/api/v2/torrents/renameFolder", form).ConfigureAwait(false);
            await EnsureSuccess(resp).ConfigureAwait(false);
        }

        public async Task<IList<TorrentPeer>> GetTorrentPeersAsync(string hash)
        {
            return await GetJsonAsync<IList<TorrentPeer>>($"/api/v2/torrents/peers?hash={hash}").ConfigureAwait(false);
        }

        public async Task SetTorrentTagsAsync(IEnumerable<string> hashes, IEnumerable<string> tags)
        {
            var resp = await PostFormAsync("/api/v2/torrents/setTags", new[]
            {
                new KeyValuePair<string, string>("hashes", string.Join("|", hashes)),
                new KeyValuePair<string, string>("tags", string.Join(",", tags))
            }).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task SetTorrentPriorityAsync(IEnumerable<string> hashes, int priority)
        {
            var resp = await PostFormAsync("/api/v2/torrents/setPriority", new[]
            {
                new KeyValuePair<string, string>("hashes", string.Join("|", hashes)),
                new KeyValuePair<string, string>("priority", priority.ToString())
            }).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task SetTorrentLocationAsync(IEnumerable<string> hashes, string location)
        {
            var resp = await PostFormAsync("/api/v2/torrents/setLocation", new[]
            {
                new KeyValuePair<string, string>("hashes", string.Join("|", hashes)),
                new KeyValuePair<string, string>("location", location)
            }).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<IList<TorrentContent>> GetTorrentContentAsync(string hash)
        {
            return await GetJsonAsync<IList<TorrentContent>>($"/api/v2/torrents/content?hash={hash}").ConfigureAwait(false);
        }

        private static string JoinHashes(IEnumerable<string> hashes)
        {
            if (hashes == null) return null;
            var arr = hashes as string[] ?? hashes.ToArray();
            if (!arr.Any()) return null;
            return string.Join("|", arr);
        }

        private static string JoinIndexes(IEnumerable<int> idx)
        {
            if (idx == null) return null;
            var arr = idx as int[] ?? idx.ToArray();
            if (!arr.Any()) return null;
            return string.Join("|", arr);
        }

        private async Task EnsureSuccess(HttpResponseMessage resp)
        {
            if (resp == null) throw new ArgumentNullException(nameof(resp));
            resp.EnsureSuccessStatusCode();
        }
    }
}