/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 мая 2026 19:18:20
 * Version: 1.0.37
 */

namespace LizeriumFindChanges.Components
{
    /// <summary>
    /// Файл
    /// </summary>
    public class FileChange
    {
        /// <summary>
        /// Путь до файла
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Тип изменения
        /// </summary>
        public ChangeType Type { get; set; }
        /// <summary>
        /// Файл или папка
        /// </summary>
        public bool IsDirectory { get; set; }
        /// <summary>
        /// Старый путь до папки или файла
        /// </summary>
        public string? OldPath { get; set; }
    }
}
