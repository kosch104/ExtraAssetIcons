using Extra.Lib;
using Extra.Lib.Helper;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;

namespace ExtraAssetIcons.Mod
{
	internal static class EditEntities
	{
		internal static void SetupEditEntities()
		{
			EntityQueryDesc spawnableBuildingEntityQueryDesc = new EntityQueryDesc
			{
				Any =
				[
					ComponentType.ReadOnly<SpawnableBuildingData>(),
					ComponentType.ReadOnly<PlaceholderBuildingData>()
				]
			};

			ExtraLib.AddOnEditEnities(new(OnEditSpawnableBuildingEntities, spawnableBuildingEntityQueryDesc));
		}

		private static void Log(string message)
		{
			EAI.Logger.Info(message);
		}

		private static void OnEditSpawnableBuildingEntities(NativeArray<Entity> entities)
		{
			int i = 1;
			foreach (Entity entity in entities)
			{
				//ENA.Logger.Info("Spawnable Building: " + i);
				if (ExtraLib.m_PrefabSystem.TryGetPrefab(entity, out ObjectPrefab prefab))
				{
					//ENA.Logger.Info("Building " + i + " parsed. Prefab: " + prefab);
					var prefabUI = prefab.GetComponent<UIObject>();
					if (prefabUI == null)
					{
						prefabUI = prefab.AddComponent<UIObject>();
						prefabUI.active = true;
						prefabUI.m_IsDebugObject = false;
						prefabUI.m_Icon = Icons.GetIcon(prefab, false);
						prefabUI.m_Priority = 1;

					}
					//prefabUI.m_Group?.RemoveElement(entity);
					prefabUI.m_Group = PrefabsHelper.GetOrCreateUIAssetCategoryPrefab("Landscaping", "Spawnable Buildings", Icons.GetIcon);
					prefabUI.m_Group.AddElement(entity);

					ExtraLib.m_EntityManager.AddOrSetComponentData(entity, prefabUI.ToComponentData());
				}

				i++;
			}
		}
	}
}
