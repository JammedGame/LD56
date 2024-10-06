using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	public GoodUnit TestGoodUnit;

	private NightBattleContext currentBattle;
	private PlayerBattleInputManager currentBattleInputManager;

	public YouWinUI YouWinUI;
	public YouLoseUI YouLoseUI;
	
	void Start()
	{
		// read from user state.
		UserBattleData = new();

		// TEMP DATA START (delete this)
		UserState.Instance.SpellBookState.AddSpell(new UserEquippedSpell(GameSettings.Spells[0], 1));
		UserState.Instance.SpellBookState.AddSpell(new UserEquippedSpell(GameSettings.Spells[0], 2));
		UserState.Instance.SpellBookState.AddSpell(new UserEquippedSpell(GameSettings.Spells[0], 3));
		UserState.Instance.SpellBookState.AddSpell(new UserEquippedSpell(GameSettings.Spells[0], 4));
		UserState.Instance.ArmyState.AddUnit(new UnitState(0, new UserUnitInfo(TestGoodUnit, 0)));
		UserState.Instance.WallState.level = 0;
		UserState.Instance.WallState.currentHealthNormalized = 1f;
		// TEMP DATA END

		UserBattleData.EquippedSpells.AddRange(UserState.Instance.SpellBookState.Spells);

		UserBattleData.UserUnits.AddRange(UserState.Instance.ArmyState.GetLivingUnits().Select(x => x.info));

		UserBattleData.WallState = UserState.Instance.WallState;

		currentBattle = new NightBattleContext(GameSettings.Levels[0], UserBattleData, Wall);
		
		currentBattleInputManager = Instantiate(GameSettings.BattleInputManagerPrefab);
		currentBattleInputManager.Setup(currentBattle, UserBattleData);

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
			
			SceneManager.LoadScene("DayScene");
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
