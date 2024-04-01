using Colossal.Logging;
using Extra.Lib.Debugger;
using Game;
using Game.Modding;
using Game.SceneFlow;
using HarmonyLib;
using System.IO;
using System.Linq;

namespace ExtraAssetIcons
{
	public class EAI : IMod
	{
		private static readonly ILog log = LogManager.GetLogger($"{nameof(ExtraAssetIcons)}").SetShowsErrorsInUI(false);
		internal static Logger Logger { get; private set; } = new(log, true);

		internal static string ResourcesIcons { get; private set; }

		private Harmony harmony;

		public void OnLoad(UpdateSystem updateSystem)
		{
			Logger.Info(nameof(OnLoad));

			if (!GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset)) return;

			Logger.Info($"Current mod asset at {asset.path}");

			FileInfo fileInfo = new(asset.path);

			ResourcesIcons = Path.Combine(fileInfo.DirectoryName, "Icons");

			EditEntities.SetupEditEntities();
			Icons.LoadIcons(fileInfo.DirectoryName);

			harmony = new($"{nameof(ExtraAssetIcons)}.{nameof(EAI)}");
			harmony.PatchAll(typeof(EAI).Assembly);
			var patchedMethods = harmony.GetPatchedMethods().ToArray();
			Logger.Info($"Plugin ExtraAssetIcons made patches! Patched methods: " + patchedMethods.Length);
			foreach (var patchedMethod in patchedMethods)
			{
				Logger.Info($"Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
			}

		}

		public void OnDispose()
		{
			Logger.Info(nameof(OnDispose));
			harmony.UnpatchAll($"{nameof(ExtraAssetIcons)}.{nameof(EAI)}");
		}
	}
}
