/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 02 сентября 2026 06:58:54
 * Version: 1.0.160
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
