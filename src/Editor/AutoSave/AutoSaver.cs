using System.IO;
using Appalachia.Utility.AutoSave.Configuration;
using Appalachia.Utility.AutoSave.Metadata;
using Appalachia.Utility.Strings;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.AutoSave
{
    internal static class AutoSaver
    {
        public static void Execute()
        {
            var scene = SceneManager.GetActiveScene();

            if (!scene.isDirty)
            {
                return;
            }

            SaveCurrentScene(scene);
            OrganizeExistingSaves();
        }

        public static string GetAutoSaveFilePath(AutoSaveMetadata metadata, string relativeSavePath)
        {
            var saveNameIdentifier = AutoSaverConfiguration.FileName;
            var autosaveFileName = metadata.GetSaveFileName(saveNameIdentifier);

            var filename = ZString.Format("{0}.unity",     autosaveFileName);
            var finalOutputPath = ZString.Format("{0}{1}", relativeSavePath, filename);

            return finalOutputPath;
        }

        private static void OrganizeExistingSaves()
        {
            var allSaveFiles = AutoSaverIO.GetAutoSaveFiles();
            var saveCollection = new AutoSaveMetadataCollection(allSaveFiles);
            var saveNameIdentifier = AutoSaverConfiguration.FileName;

            foreach (var scene in saveCollection.scenes.Keys)
            {
                var saves = saveCollection.scenes[scene];

                saves.SortSaves();

                while (saves.saves.Count > AutoSaverConfiguration.FilesCount)
                {
                    var first = saves.saves[0];
                    saves.saves.RemoveAt(0);

                    var relativeBase = AutoSaverConfiguration.GetRelativeSaveDirectory();
                    var relativePath = Path.Combine(relativeBase, first.fileName) + ".unity";

                    AssetDatabase.DeleteAsset(relativePath);
                }

                saves.UpdateNames(saveNameIdentifier);
            }
        }

        private static void SaveCurrentScene(Scene scene)
        {
            var newSave = new AutoSaveMetadata(scene);

            var relativeSavePath = AutoSaverConfiguration.GetRelativeSaveDirectory();

            var saveFilePath = GetAutoSaveFilePath(newSave, relativeSavePath);

            AutoSaverIO.SaveScene(scene, saveFilePath);

            AutoSaverConfiguration.LastSave = AutoSaverConfiguration.EditorTimer;
        }
    }
}
