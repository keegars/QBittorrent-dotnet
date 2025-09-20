# qBittorrent 5.0+ .NET Wrapper

A fully-featured **.NET Framework 4.8.1 library** for interacting with the [qBittorrent Web API](https://github.com/qbittorrent/qBittorrent/wiki/WebUI-API-%28qBittorrent-5.0%29). Designed for developers who want **type-safe, asynchronous access** to qBittorrent from .NET applications.

---

## Features

* **Comprehensive API Coverage**

  * **Torrents**: Add, pause, resume, delete, recheck, and reannounce torrents.
  * **Logs**: Retrieve, filter, and monitor logs.
  * **Sync**: Synchronize settings and preferences.
  * **Application**: Access qBittorrent version, build info, and manage preferences.
  * **Global Transfer**: Monitor overall download/upload speeds, totals, and connections.
  * **Categories**: Create, list, and remove categories.
  * **Tags**: Create, list, and remove tags.

* **Asynchronous Programming**: Fully async methods using `Task` for smooth integration in modern .NET apps.

* **Type-Safe DTOs**: Strongly-typed models for torrents, logs, global transfer info, preferences, and more.

* **Session Management**: Handles login/logout and cookies automatically with `IDisposable`.

* **Open Source**: Licensed under **GPL-3.0**. Modifications must remain open source and credit is preserved.

---

## Installation

1. **Clone the repository**:

```bash
git clone https://github.com/keegars/QBittorrent-dotnet.git
```

2. **Add the project to your solution**:

* Open your solution in Visual Studio.
* Right-click the solution → Add → Existing Project → select the cloned project.

3. **Add references**:

* Reference the project in your application to use the APIs.

---

## Quick Start

### Initialize the Client

```csharp
using QBittorrent.Client;

var client = new QBittorrentClient("http://localhost:8080");
```

### Authenticate

```csharp
await client.App.LoginAsync("admin", "password");
```

---

### Torrents API

```csharp
// Add a torrent
await client.Torrents.AddTorrentAsync("magnet:?xt=urn:btih:...");

// Pause/resume torrents
await client.Torrents.PauseTorrentsAsync(new [] { "HASH1", "HASH2" });
await client.Torrents.ResumeTorrentsAsync(new [] { "HASH1", "HASH2" });

// Delete torrents
await client.Torrents.DeleteTorrentsAsync(new [] { "HASH1" }, deleteFiles: true);

// Recheck/Reannounce torrents
await client.Torrents.RecheckTorrentsAsync(new [] { "HASH1" });
await client.Torrents.ReannounceTorrentsAsync(new [] { "HASH1" });

// Get torrent details
var torrents = await client.Torrents.GetTorrentsAsync();
var properties = await client.Torrents.GetTorrentPropertiesAsync("HASH1");
var files = await client.Torrents.GetTorrentFilesAsync("HASH1");
```

---

### Logs API

```csharp
var logs = await client.Log.GetAsync();
```

---

### Sync API

```csharp
await client.Sync.RefreshAsync();
```

---

### Application API

```csharp
var version = await client.App.GetVersionAsync();
var webApiVersion = await client.App.GetWebApiVersionAsync();
var build = await client.App.GetBuildInfoAsync();

// Preferences
var preferences = await client.App.GetPreferencesAsync();
preferences.AutoTmmEnabled = true;
await client.App.SetPreferencesAsync(preferences);
```

---

### Global Transfer API

```csharp
var transferInfo = await client.GlobalTransfer.GetGlobalTransferInfoAsync();
Console.WriteLine($"Download Speed: {transferInfo.DownloadSpeed} bytes/sec");
Console.WriteLine($"Upload Speed: {transferInfo.UploadSpeed} bytes/sec");
```

---

### Categories API

```csharp
await client.Categories.AddCategoryAsync("Movies", "/path/to/movies");
var categories = await client.Categories.GetCategoriesAsync();
await client.Categories.RemoveCategoryAsync("Movies");
```

---

### Tags API

```csharp
await client.Tags.AddTagAsync("Action");
var tags = await client.Tags.GetTagsAsync();
await client.Tags.RemoveTagAsync("Action");
```

---

## Notes & Tips

* **Dispose the client**: `QBittorrentClient` implements `IDisposable`. Always wrap it in a `using` statement or call `Dispose()` when finished to free resources.

```csharp
using (var client = new QBittorrentClient("http://localhost:8080"))
{
    // Use the client
}
```

* **Async all the way**: All API calls are asynchronous. Avoid blocking calls (`.Result` or `.Wait()`) to prevent deadlocks in GUI or web applications.

* **Session handling**: The client manages cookies automatically, so you do not need to manually maintain session IDs.

* **Error handling**: API calls will throw exceptions for HTTP errors. Use `try/catch` to handle network or server issues gracefully.

* **Boolean values in preferences**: When modifying preferences, always provide `true`/`false` values correctly, as strings are used in the Web API POST payload.

* **Logging**: Use the `Log` API to monitor errors or warnings from qBittorrent itself for better diagnostics.

* **Thread safety**: The `HttpClient` instance is shared across all APIs, so avoid modifying the `HttpClient` directly while making concurrent calls.

---

## License

This library is licensed under the **[GPL-3.0 License](LICENSE)**.

* You may freely **use, modify, and redistribute** it.
* Any derivative works must also be **GPL-3.0 licensed**.
* Credit to the original author **must be preserved**.
