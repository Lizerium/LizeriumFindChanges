/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 ноября 2026 06:53:09
 * Version: 1.0.232
 */

namespace LizeriumFindChanges.Components
{
    /// <summary>
    /// Тип изменения
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// Добавлено
        /// </summary>
        Added,
        /// <summary>
        /// Удалено
        /// </summary>
        Deleted,
        /// <summary>
        /// Изменено
        /// </summary>
        Changed,
        /// <summary>
        /// Неизменено
        /// </summary>
        Unchanged,
        /// <summary>
        /// Перемещено
        /// </summary>
        Retranslate,
    }
}
