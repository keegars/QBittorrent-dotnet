using QBittorrent.Client;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CategoryDTO = Qbittorrent_dotnet.DTO.Category.Category;

namespace Qbittorrent_dotnet.Category
{
    public class CategoryApi : ApiClientBase, ICategoryApi
    {
        public CategoryApi(HttpClient httpClient, string baseUrl, CookieContainer cookieContainer)
            : base(httpClient, baseUrl, cookieContainer)
        {
        }

        public async Task<IDictionary<string, CategoryDTO>> GetCategoriesAsync()
        {
            return await GetJsonAsync<IDictionary<string, CategoryDTO>>("/api/v2/torrents/categories").ConfigureAwait(false);
        }

        public async Task AddCategoryAsync(string name, string savePath)
        {
            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("category", name),
                new KeyValuePair<string, string>("savePath", savePath)
            };

            var resp = await PostFormAsync("/api/v2/torrents/createCategory", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }

        public async Task RemoveCategoryAsync(string name)
        {
            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("category", name)
            };

            var resp = await PostFormAsync("/api/v2/torrents/removeCategories", form).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}