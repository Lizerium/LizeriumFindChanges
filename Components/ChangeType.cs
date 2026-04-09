/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 09 апреля 2026 10:59:00
 * Version: 1.0.13
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
