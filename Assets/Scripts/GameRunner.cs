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
		UserBattleData = UserState.Instance.GetUserBattleData();

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
			UserState.Instance.ApplyBattleWinResults(battleContext);

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
