/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 апреля 2026 06:54:25
 * Version: 1.0.28
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
