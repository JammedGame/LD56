using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
	public Image FillBar;
	public NightBattleContext CurrentBattle;
	
	public void Setup(NightBattleContext currentBattle)
	{
		CurrentBattle = currentBattle;
	}

	void Update()
	{
		if (CurrentBattle == null)
			return;
		
		float healthPercent = CurrentBattle.Wall.IsAlive() ? CurrentBattle.Wall.HealthPercent : 0f;
		FillBar.fillAmount = healthPercent;
	}
}
