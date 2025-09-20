using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Helpers
{
    internal static class CachedReflectionMap
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _cache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Gets all public instance properties of a type, cached for efficiency.
        /// </summary>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return _cache.GetOrAdd(type, t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance));
        }

        /// <summary>
        /// Preloads all DTO classes under the QBittorrent.Client.DTO namespace that have JsonProperty attributes.
        /// </summary>
        public static void PreloadDtoProperties()
        {
            var dtoTypes = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(t => t.IsClass
                                               && t.Namespace == nameof(Qbittorrent_dotnet) + nameof(Qbittorrent_dotnet.DTO)
                                               && t.GetProperties().Any(p => p.GetCustomAttribute<JsonPropertyAttribute>() != null));

            foreach (var type in dtoTypes)
            {
                _cache[type] = type.GetProperties()
                                   .Where(p => p.GetCustomAttribute<JsonPropertyAttribute>() != null)
                                   .ToArray();
            }
        }

    }
}
