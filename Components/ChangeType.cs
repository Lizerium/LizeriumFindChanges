/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 19 августа 2026 10:24:04
 * Version: 1.0.146
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
