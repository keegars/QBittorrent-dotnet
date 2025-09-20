using Newtonsoft.Json;

namespace Qbittorrent_dotnet.DTO.App
{
    /// <summary>
    /// Represents build and environment information for the running qBittorrent instance.
    /// </summary>
    public class BuildInfo
    {
        /// <summary>
        /// Gets the version of the Qt framework used to build qBittorrent.
        /// </summary>
        [JsonProperty("qt")]
        public string Qt { get; set; }

        /// <summary>
        /// Gets the version of libtorrent used by qBittorrent.
        /// </summary>
        [JsonProperty("libtorrent")]
        public string Libtorrent { get; set; }

        /// <summary>
        /// Gets the version of Boost libraries used by qBittorrent.
        /// </summary>
        [JsonProperty("boost")]
        public string Boost { get; set; }

        /// <summary>
        /// Gets the version of OpenSSL used by qBittorrent.
        /// </summary>
        [JsonProperty("openssl")]
        public string OpenSsl { get; set; }

        /// <summary>
        /// Gets the bitness of the application (e.g. 64 for 64-bit builds).
        /// </summary>
        [JsonProperty("bitness")]
        public int Bitness { get; set; }
    }

}