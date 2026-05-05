/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 05 мая 2026 07:03:03
 * Version: 1.0.40
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
