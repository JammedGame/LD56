using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Night;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
	public GameSettings GameSettings;
	public UserBattleData UserBattleData;
	public Wall Wall;

	private NightBattleContext currentBattle;
	private PlayerBattleInputManager currentBattleInputManager;
	
	void Start()
	{
		// read from user state.
		UserBattleData = new();
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 1));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 2));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 3));
		UserBattleData.EquippedSpells.Add(new UserEquippedSpell(GameSettings.Spells[0], 4));
		
		currentBattle = new NightBattleContext(GameSettings.Levels[0], UserBattleData, Wall);
		
		currentBattleInputManager = Instantiate(GameSettings.BattleInputManagerPrefab);
		currentBattleInputManager.Setup(currentBattle, UserBattleData);

		StartCoroutine(BattleFlow(currentBattle));
	}

	IEnumerator BattleFlow(NightBattleContext battleContext)
	{
		while (battleContext.Winner == null)
		{
			try
			{
				battleContext.TickBattle();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			
			yield return null;
		}

		if (battleContext.Winner == Team.Good)
		{
			yield return YouWin();
		}
		else
		{
			yield return YouLose();
		}
	}

	private IEnumerator YouWin()
	{
		Debug.Log("YOU WIN!!!!");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("DayScene");
	}

	private IEnumerator YouLose()
	{
		Debug.Log("YOU LOSE!!!!");
		yield return new WaitForSeconds(1f);
	}
}
