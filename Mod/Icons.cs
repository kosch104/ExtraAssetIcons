using System.IO;
using Game.Prefabs;

namespace ExtraAssetIcons.Mod
{
    internal class Icons
    {
        internal const string IconsResourceKey = "ExtraAssetIcons";
        internal static readonly string COUIBaseLocation = $"coui://{IconsResourceKey}";

        public static readonly string Placeholder = $"{COUIBaseLocation}/Icons/Misc/placeholder.svg";
        public static readonly string GameCrashWarning = $"{COUIBaseLocation}/Icons/Misc/GameCrashWarning.svg";

        internal static void LoadIcons(string path)
        {
            Extra.Lib.UI.Icons.LoadIconsFolder(IconsResourceKey, path);
        }

        public static string GetIcon(PrefabBase prefab)
        {
            return GetIcon(prefab, true);
        }

        public static string GetIcon(PrefabBase prefab, bool replaceWithPlaceholder = true)
        {

            if (prefab is null)
                if (replaceWithPlaceholder)
                    return $"{COUIBaseLocation}/Icons/Misc/placeholder.svg";
                else
                    return null;

            if (File.Exists($"{EAI.ResourcesIcons}/{prefab.GetType().Name}/{prefab.name}.svg"))
            {
                //ENA.Logger.Info($"Found icon in mod folder: {ENA.ResourcesIcons}/{prefab.GetType().Name}/{prefab.name}.svg");
                return $"{COUIBaseLocation}/Icons/{prefab.GetType().Name}/{prefab.name}.svg";
            }
            if (File.Exists($"{EAI.ResourcesIcons}/{prefab.GetType().Name}/{prefab.name}.png"))
            {
                return $"{COUIBaseLocation}/Icons/{prefab.GetType().Name}/{prefab.name}.png";
            }

            if (!replaceWithPlaceholder)
                return null;

            //ENA.Logger.Info($"Did not find icon in mod folder: {ENA.ResourcesIcons}/{prefab.GetType().Name}/{prefab.name}.svg");

            if (prefab is UIAssetCategoryPrefab)
            {

                return $"{COUIBaseLocation}/Icons/Misc/placeholder.svg";
            }
            else if (prefab is UIAssetMenuPrefab)
            {

                return $"{COUIBaseLocation}/Icons/Misc/placeholder.svg";
            }

            return Placeholder;
        }

    }
}
