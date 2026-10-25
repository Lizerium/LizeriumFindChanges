/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 25 октября 2026 09:06:34
 * Version: 1.0.212
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
