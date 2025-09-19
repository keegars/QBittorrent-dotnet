using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Tag
{
    public interface ITagApi
    {
        Task<IList<string>> GetTagsAsync();

        Task AddTagAsync(string tag);

        Task RemoveTagAsync(string tag);
    }
}