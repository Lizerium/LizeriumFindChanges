/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 30 марта 2026 12:21:05
 * Version: 1.0.2
 */

namespace LizeriumFindChanges.Components
{
    /// <summary>
    /// Структура данных сравнения
    /// </summary>
    public class ChangesData
    {
        public List<FileChange> Files { get; set; } = new List<FileChange>();

        public override string ToString()
        {
            var AddedIsDir = Files.Where(f => f.Type == ChangeType.Added && f.IsDirectory).ToArray();
            var Added = Files.Where(f => f.Type == ChangeType.Added && !f.IsDirectory).ToArray();
            var DeletedIsDir = Files.Where(f => f.Type == ChangeType.Deleted && f.IsDirectory).ToArray();
            var Deleted = Files.Where(f => f.Type == ChangeType.Deleted && !f.IsDirectory).ToArray();
            var ChangedIsDir = Files.Where(f => f.Type == ChangeType.Changed && f.IsDirectory).ToArray();
            var Changed = Files.Where(f => f.Type == ChangeType.Changed && !f.IsDirectory).ToArray();
            var RetranslateIsDir = Files.Where(f => f.Type == ChangeType.Retranslate && f.IsDirectory).ToArray();
            var Retranslate = Files.Where(f => f.Type == ChangeType.Retranslate && !f.IsDirectory).ToArray();
            var UnchangedIsDir = Files.Where(f => f.Type == ChangeType.Unchanged && f.IsDirectory).ToArray();
            var Unchanged = Files.Where(f => f.Type == ChangeType.Unchanged && !f.IsDirectory).ToArray();

            var res = "{\"AddedDirectory\":[" +
                string.Join(",", AddedIsDir.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"AddedFiles\":[" +
                string.Join(",", Added.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"DeletedDirectory\":[" +
                string.Join(",", DeletedIsDir.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"DeletedFiles\":[" +
                string.Join(",", Deleted.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"ChangedDirectory\":[" +
                string.Join(",", ChangedIsDir.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"ChangedFiles\":[" +
                string.Join(",", Changed.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"RetranslateDirectory\":[" +
                string.Join(",", RetranslateIsDir.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "], \"RetranslateFiles\":[" +
                string.Join(",", RetranslateIsDir.Select(f => "\"" + f.Path.Replace("\\", "\\\\") + "\"").ToArray()) +
                "]}";

            return res;
        }
    }
}
