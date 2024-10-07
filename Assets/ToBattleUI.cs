using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToBattleUI : MonoBehaviour
{
	public Button Button;
	
	public void OnClick()
	{
		if (UserState.Instance.SpellBookState.EquippedSpells.Count > 0)
		{
			SceneManager.LoadScene("StegaTest");
		}
	}

	public void Update()
	{
		Button.gameObject.SetActive(UserState.Instance.SpellBookState.EquippedSpells.Count > 0);
	}
}
