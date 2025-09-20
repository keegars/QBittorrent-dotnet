using Newtonsoft.Json;
using Qbittorrent_dotnet.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Helpers
{
    internal static class KeyValuePairHelper
    {
        /// <summary>
        /// Builds a list of key-value pairs for form submission from any DTO
        /// with [JsonProperty] attributes, handling nulls and booleans automatically.
        /// </summary>
        public static List<KeyValuePair<string, string>> BuildForm(object dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var form = new List<KeyValuePair<string, string>>();

            // Get cached properties for this type
            var properties = CachedReflectionMap.GetProperties(dto.GetType());

            foreach (var prop in properties)
            {
                // Optional: skip ignored properties
                if (prop.GetCustomAttribute<FormIgnoreAttribute>() != null) continue;

                var value = prop.GetValue(dto);

                string stringValue;
                if (value == null)
                    stringValue = string.Empty;
                else if (value is bool b)
                    stringValue = b.ToString().ToLower();
                else
                    stringValue = value.ToString();

                var key = prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? prop.Name;

                form.Add(new KeyValuePair<string, string>(key, stringValue));
            }

            return form;
        }
    }
}
