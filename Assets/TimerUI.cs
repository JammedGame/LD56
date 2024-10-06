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
	}	
}
