using Qbittorrent_dotnet;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QBittorrentTorrentAnalyzer
{
    internal class Program
    {
        // Configure WebUI credentials here
        private static string qbBaseUrl = "http://localhost:8080";
        private static string qbApiUrl = qbBaseUrl + "/api/v2/";
        private static string qbUser = "admin";
        private static string qbPass = "";

        private static async Task Main(string[] args)
        {
            string torrentHash = string.Empty;

            //Prod environment, check arguments and put torrent has as args[0]
            if (!Debugger.IsAttached)
            {
                if (args.Length < 1)
                {
                    Console.WriteLine("Arg Usage: <torrentHash>");
                    return;
                }

                torrentHash = args[0];
            }
            //Debugger attached
            else
            {
                using (IQBittorrentClient client = new QBittorrentClient(qbBaseUrl))
                {
                    await client.Auth.LoginAsync(qbUser, qbPass);

                    var torrents = await client.Torrents.GetTorrentsAsync();

                    foreach (var torrent in torrents)
                    {
                        var files = await client.Torrents.GetTorrentFilesAsync(torrent.Hash);
                    }
                }
            }
        }
    }
}