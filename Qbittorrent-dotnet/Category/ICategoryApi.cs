using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryDTO = Qbittorrent_dotnet.DTO.Category.Category;

namespace Qbittorrent_dotnet.Category
{
    public interface ICategoryApi
    {
        Task<IDictionary<string, CategoryDTO>> GetCategoriesAsync();

        Task AddCategoryAsync(string name, string savePath);

        Task RemoveCategoryAsync(string name);
    }
}