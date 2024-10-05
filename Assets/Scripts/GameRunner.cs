using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Night;
using Unity.VisualScripting;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
	public GameSettings GameSettings;
	public UserBattleData UserBattleData;

	NightBattleContext currentBattle;
	
	void Start()
	{
		UserBattleData = new();
		currentBattle = new NightBattleContext(GameSettings.Levels[0], UserBattleData);
	}
	
	void Update()
	{
		currentBattle.TickBattle();
	}
}
