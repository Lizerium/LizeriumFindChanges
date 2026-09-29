/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 29 сентября 2026 09:42:34
 * Version: 1.0.187
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
