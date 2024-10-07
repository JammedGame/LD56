using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackUI : MonoBehaviour
{
	public void GoBack()
	{
		UserState.Reset();
		SceneManager.LoadScene("Menu");
	}
}
