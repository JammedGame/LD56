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

	private NightBattleContext currentBattle;
	private PlayerBattleInputManager currentBattleInputManager;
	
	void Start()
	{
		UserBattleData = new();
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 1));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 2));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 3));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 4));
		
		currentBattle = new NightBattleContext(GameSettings.Levels[0], UserBattleData);
		
		currentBattleInputManager = Instantiate(GameSettings.BattleInputManagerPrefab);
		currentBattleInputManager.Setup(currentBattle, UserBattleData);
	}
	
	void Update()
	{
		currentBattle.TickBattle();
	}
}
