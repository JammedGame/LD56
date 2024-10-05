using System.Collections.Generic;
using Night;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu]
	public class GameSettings : ScriptableObject
	{
		public List<NightLevelData> Levels;
		
	}
}