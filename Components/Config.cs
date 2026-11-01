/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 01 ноября 2026 11:17:06
 * Version: 1.0.219
 */

namespace LizeriumFindChanges.Components
{
    record Config
    {
        public string Before { get; init; } = "";
        public string After { get; init; } = "";
        public string Version { get; init; } = "";
        public bool IsMissingFilesMode { get; init; } = false;
    }
}
