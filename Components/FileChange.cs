/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 08 апреля 2026 14:28:35
 * Version: 1.0.12
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
