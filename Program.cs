/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 16:23:10
 * Version: 1.0.25
 */

using System.Diagnostics;
using System.Text.Json;

using LizeriumFindChanges.Components;
using LizeriumFindChanges.Services;

const string configFileName = "config.json";

var config = await LoadOrCreateConfigAsync(configFileName);
if (config == null)
{
    Console.WriteLine("[ERROR] Invalid config parameters. Please check the config.json file.");
    return;
}

Console.WriteLine($"[x] Put [BEFORE]: {config.Before}");
Console.WriteLine($"[x] Put [AFTER]: {config.After}");
Console.WriteLine($"[x] Put [VERSION]: {config.Version}");
Console.WriteLine($"[x] Put [IS_MISSING_MODE]: {config.IsMissingFilesMode}");

Console.WriteLine($"[*]>Started...");

Stopwatch stopwatch = Stopwatch.StartNew();

var comparer = new CompareService();


if(config.IsMissingFilesMode)
{
    var compareMissingFilesFolder = "MISSING";
    if (!Directory.Exists(compareMissingFilesFolder))
        Directory.CreateDirectory(compareMissingFilesFolder);
    await comparer.ExtractMissingFilesFromBeforeAsync(
        beforeDir: config.Before,
        afterDir: config.After,
        outputDir: compareMissingFilesFolder);
}
else
{
    var data = await comparer.CompareAndLogChangesExtended(config.Before, config.After);
    File.WriteAllText("manifest.json", data.ToString());
    await comparer.CreateUpdateFolderAsync(data, config.After, config.Version);
}

stopwatch.Stop();

Console.WriteLine($"[*]>Success! [{stopwatch.ElapsedMilliseconds / 1000} s]");

// --- Методы ---

async Task<Config?> LoadOrCreateConfigAsync(string path)
{
    if (!File.Exists(path))
    {
        // Создаем шаблонный файл с пустыми значениями
        var template = new Config
        {
            Before = "",
            After = "",
            Version = ""
        };
        var json = JsonSerializer.Serialize(template, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json);
        Console.WriteLine($"[INFO] Config file created at '{path}'. Please fill in the required fields.");
        return null; // не продолжаем дальше, ждем чтобы пользователь заполнил
    }

    try
    {
        var json = await File.ReadAllTextAsync(path);
        var config = JsonSerializer.Deserialize<Config>(json);

        // Проверяем на валидность - пусть параметры не пустые
        if (config == null
            || string.IsNullOrWhiteSpace(config.Before)
            || string.IsNullOrWhiteSpace(config.After)
            || string.IsNullOrWhiteSpace(config.Version))
        {
            return null;
        }

        return config;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] Failed to read or parse config file: {ex.Message}");
        return null;
    }
}
