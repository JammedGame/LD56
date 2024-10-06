using System;
using System.Collections;
using System.Collections.Generic;
using Night;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
	NightBattleContext _battleContext;
	
	[SerializeField] private TMP_Text timerText;
	[SerializeField] private TMP_Text enemiesRemainingText;

	private int lastRemainingMobs = -1;
	
	public void Setup(NightBattleContext currentBattle)
	{
		_battleContext = currentBattle;
	}

	void Update()
	{
		if (_battleContext == null)
			return;

		TimeSpan timeRemaining = TimeSpan.FromSeconds(_battleContext.LevelDuration - _battleContext.GameTime);
		timerText.text = $"{timeRemaining.Minutes:00} : {timeRemaining.Seconds:00}";
		
		int enemiesRemaining = _battleContext.MobSpawner.GetEnemiesRemainingCount();
		if (lastRemainingMobs != enemiesRemaining)
		{
			lastRemainingMobs = enemiesRemaining;
			enemiesRemainingText.text = $"{enemiesRemaining} mobs remaining";
		}
	}	
}
