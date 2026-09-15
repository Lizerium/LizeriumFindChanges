/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 15 сентября 2026 07:38:45
 * Version: 1.0.173
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
