using Night;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor
{
	[CustomEditor(typeof(NightLevelData))]
	public class WaveSpawnHelper : UnityEditor.Editor
	{
		private NightLevelData script;

		private void OnEnable()
		{
			script = (NightLevelData)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Add Random Unit"))
			{
				script.Spawns.Add(
					new WaveSpawn()
					{
						Time = Random.Range(0, 3f),
						UnitLevel = 0,
						UnitPrefab = script.Spawns[0].UnitPrefab,
						SpawnPosition = new Vector3(Random.Range(8f, 11f), Random.Range(0, 0), Random.Range(-6f, 6f))
					}
				);
			}
		}
	}
}