/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 16:23:10
 * Version: 1.0.25
 */

using LizeriumFindChanges.Components;

namespace LizeriumFindChanges.Services
{
    public class CompareService
    {
        /// <summary>
        /// Сравнение файлов и папок двух папок
        /// </summary>
        /// <param name="beforeDir">До</param>
        /// <param name="afterDir">После</param>
        /// <returns>ChangesData</returns>
        public async Task<ChangesData> CompareAndLogChangesExtended(string beforeDir, string afterDir)
        {
            try
            {
                var result = new List<FileChange>();
                // Сначала сравниваем директории
                result.AddRange(CompareDirectories(beforeDir, afterDir));
                // Потом сравниваем файлы
                result.AddRange(await CompareFiles(beforeDir, afterDir));
                return new ChangesData() { Files = result };
            }
            catch (Exception ex)
            {
                return new ChangesData() { };
            }
        }

        /// <summary>
        /// Находит файлы, которые были в beforeDir, но отсутствуют в afterDir,
        /// и копирует их в outputDir, сохраняя структуру директорий.
        /// </summary>
        /// <param name="beforeDir">Исходная директория</param>
        /// <param name="afterDir">Сравниваемая директория</param>
        /// <param name="outputDir">Куда копировать новые файлы</param>
        public async Task ExtractMissingFilesFromBeforeAsync(string beforeDir, string afterDir, string outputDir)
        {
            var beforeFiles = Directory.GetFiles(beforeDir, "*", SearchOption.AllDirectories)
                .ToDictionary(f => f.Replace(beforeDir, "").TrimStart(Path.DirectorySeparatorChar), f => f);

            var afterFiles = Directory.GetFiles(afterDir, "*", SearchOption.AllDirectories)
                .ToHashSet();

            int index = 0;

            foreach (var relativePath in beforeFiles.Keys)
            {
                index++;
                Console.WriteLine($"[#] scan missing files from before - {index}/{beforeFiles.Count}");

                var fullAfterPath = Path.Combine(afterDir, relativePath);

                if (!afterFiles.Contains(fullAfterPath))
                {
                    var sourceFilePath = beforeFiles[relativePath];
                    var targetFilePath = Path.Combine(outputDir, relativePath);

                    var targetDir = Path.GetDirectoryName(targetFilePath);
                    if (!Directory.Exists(targetDir))
                        Directory.CreateDirectory(targetDir!);

                    await using var sourceStream = File.OpenRead(sourceFilePath);
                    await using var targetStream = File.Create(targetFilePath);
                    await sourceStream.CopyToAsync(targetStream);

                    Console.WriteLine($"[+] copied missing file: {relativePath}");
                }
            }
        }

        /// <summary>
        /// Генерация обновления
        /// </summary>
        /// <param name="changes">Изменения</param>
        /// <param name="afterDir">Путь до сравниваемой директории</param>
        /// <param name="updateDir">Путь формирования обновления</param>
        /// <returns></returns>
        public async Task CreateUpdateFolderAsync(ChangesData changes, string afterDir, string updateDir)
        {
            foreach (var change in changes.Files)
            {
                if (change.IsDirectory && change.Type == ChangeType.Added)
                {
                    // Создаём директорию в updateDir
                    var targetDir = Path.Combine(updateDir, change.Path);
                    Directory.CreateDirectory(targetDir);
                }
                else if (!change.IsDirectory &&
                         (change.Type == ChangeType.Added || change.Type == ChangeType.Changed || change.Type == ChangeType.Retranslate))
                {
                    var sourceFilePath = Path.Combine(afterDir, change.Path);
                    var targetFilePath = Path.Combine(updateDir, change.Path);

                    // Убедимся, что путь до файла существует
                    var targetDir = Path.GetDirectoryName(targetFilePath);
                    if (!Directory.Exists(targetDir))
                        Directory.CreateDirectory(targetDir!);

                    // Копируем файл
                    await using var sourceStream = File.OpenRead(sourceFilePath);
                    await using var targetStream = File.Create(targetFilePath);
                    await sourceStream.CopyToAsync(targetStream);
                }
            }
        }


        /// <summary>
        /// Сравнение файлов двух папок
        /// </summary>
        /// <param name="beforeDir">Сравниваемая директория</param>
        /// <param name="afterDir">С чем сравнием</param>
        /// <returns>List<FileChange></returns>
        private async Task<List<FileChange>> CompareFiles(string beforeDir, string afterDir)
        {
            var beforeFiles = Directory.GetFiles(beforeDir, "*", SearchOption.AllDirectories)
                .ToDictionary(f => f.Replace(beforeDir, "").TrimStart(Path.DirectorySeparatorChar), f => f);

            var afterFiles = Directory.GetFiles(afterDir, "*", SearchOption.AllDirectories)
                .ToDictionary(f => f.Replace(afterDir, "").TrimStart(Path.DirectorySeparatorChar), f => f);

            var result = new List<FileChange>();

            var matchedBefore = new HashSet<string>();
            var matchedAfter = new HashSet<string>();

            int index = 0;
            // обнаружение изменённых
            foreach (var after in afterFiles)
            {
                index++;
                Console.WriteLine($"[#] scan changes files - {index}/{afterFiles.Count}");
                if (beforeFiles.ContainsKey(after.Key))
                {
                    matchedBefore.Add(after.Key);
                    matchedAfter.Add(after.Key);

                    var beforePath = beforeFiles[after.Key];
                    var afterPath = after.Value;

                    if (!FilesAreEqual(beforePath, afterPath))
                    {
                        result.Add(new FileChange { Path = after.Key, Type = ChangeType.Changed, IsDirectory = false });
                    }
                    else
                    {
                        result.Add(new FileChange { Path = after.Key, Type = ChangeType.Unchanged, IsDirectory = false });
                    }
                }
            }
            index = 0;
            // Обнаружение удалённых
            var beforeMatchesList = beforeFiles.Keys.Except(matchedBefore);
            foreach (var before in beforeMatchesList)
            {
                index++;
                Console.WriteLine($"[#] scan delete files - {index}/{beforeMatchesList.Count()}");
                result.Add(new FileChange { Path = before, Type = ChangeType.Deleted, IsDirectory = false });
            }
            index = 0;
            var afterMatchesList = afterFiles.Keys.Except(matchedAfter);
            // Обнаружение новых
            foreach (var after in afterMatchesList)
            {
                index++;
                Console.WriteLine($"[#] scan new and retranslate files - {index}/{afterMatchesList.Count()}");
                var newFilePath = afterFiles[after];
                var newFileBytes = await File.ReadAllBytesAsync(newFilePath);

                // Пытаемся найти перемещённый файл
                var oldMatch = beforeFiles.FirstOrDefault(p =>
                    !matchedBefore.Contains(p.Key) &&
                    FilesAreEqual(p.Value, newFilePath));

                if (oldMatch.Key != null)
                {
                    matchedBefore.Add(oldMatch.Key);
                    result.Add(new FileChange
                    {
                        Path = after,
                        OldPath = oldMatch.Key,
                        Type = ChangeType.Retranslate,
                        IsDirectory = false
                    });
                }
                else
                {
                    result.Add(new FileChange { Path = after, Type = ChangeType.Added, IsDirectory = false });
                }
            }

            return result;
        }
       
        /// <summary>
        /// Сравнение файлов
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        private bool FilesAreEqual(string filePath1, string filePath2)
        {
            return File.ReadAllBytes(filePath1).SequenceEqual(File.ReadAllBytes(filePath2));
        }

        /// <summary>
        /// Сравнение директорий
        /// </summary>
        /// <param name="beforeDir">Предыдущая</param>
        /// <param name="afterDir">Следующая</param>
        /// <returns>FileChange</returns>
        private List<FileChange> CompareDirectories(string beforeDir, string afterDir)
        {
            var changes = new List<FileChange>();

            var beforeDirs = Directory.GetDirectories(beforeDir, "*", SearchOption.AllDirectories)
                .Select(d => d.Replace(beforeDir, "").TrimStart(Path.DirectorySeparatorChar)).ToHashSet();

            var afterDirs = Directory.GetDirectories(afterDir, "*", SearchOption.AllDirectories)
                .Select(d => d.Replace(afterDir, "").TrimStart(Path.DirectorySeparatorChar)).ToHashSet();

            foreach (var dir in afterDirs.Except(beforeDirs))
            {
                changes.Add(new FileChange { Path = dir, Type = ChangeType.Added, IsDirectory = true });
            }

            foreach (var dir in beforeDirs.Except(afterDirs))
            {
                changes.Add(new FileChange { Path = dir, Type = ChangeType.Deleted, IsDirectory = true });
            }

            return changes;
        }

    }
}
