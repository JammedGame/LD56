using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Spells;
using Night;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
	public GameSettings GameSettings;
	public UserBattleData UserBattleData;
	public Wall Wall;
	public GoodUnit TestGoodUnit;

	private NightBattleContext currentBattle;
	private PlayerBattleInputManager currentBattleInputManager;

	public YouWinUI YouWinUI;
	public YouLoseUI YouLoseUI;
	public HealthBarUI HealthBarUI;
	public TimerUI TimerUI;
	
	void Start()
	{
		// read from user state.
		UserBattleData = new();

		// TEMP DATA START (delete this)
		foreach (SpellBookItem spell in GameSettings.Spells)
		{
			UserEquippedSpell userEquippedSpell = new UserEquippedSpell(spell, 1);
			UserState.Instance.SpellBookState.AddSpell(userEquippedSpell);
			UserBattleData.EquippedSpells.Add(userEquippedSpell);
		}
		
		UserState.Instance.ArmyState.AddUnit(new UnitState(0, new UserUnitInfo(TestGoodUnit, 0)));
		UserState.Instance.WallState.level = 0;
		UserState.Instance.WallState.currentHealthNormalized = 1f;
		// TEMP DATA END

		UserBattleData.UserUnits.AddRange(UserState.Instance.ArmyState.GetLivingUnits().Select(x => x.info));

		UserBattleData.WallState = UserState.Instance.WallState;

		int currentLevel = UserState.Instance.DayCount;
		currentBattle = new NightBattleContext(GameSettings.Levels[currentLevel], UserBattleData, Wall);
		
		currentBattleInputManager = Instantiate(GameSettings.BattleInputManagerPrefab);
		currentBattleInputManager.Setup(currentBattle, UserBattleData);
		HealthBarUI.Setup(currentBattle);
		TimerUI.Setup(currentBattle);

		StartCoroutine(BattleFlow(currentBattle));
	}

	IEnumerator BattleFlow(NightBattleContext battleContext)
	{
		yield return BattleIntro();
		
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
			yield return YouWinUI.Animate(battleContext);

			UserState.Instance.DayCount++;

			if (UserState.Instance.DayCount < GameSettings.Levels.Count)
			{
				SceneManager.LoadScene("DayScene");
			}
			else
			{
				SceneManager.LoadScene("YouWinGameScene");
			}
		}
		else
		{
			yield return YouLoseUI.Animate(battleContext);
		}
	}

	private IEnumerator BattleIntro()
	{
		// todo: UI intro
		yield break;
	}
}
