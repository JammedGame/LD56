using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinUI : MonoBehaviour
{
	public float Duration;	
	public Animator Animator;	
	
	public IEnumerator Animate(NightBattleContext battleContext)
	{
		gameObject.SetActive(true);
		
		Animator.SetTrigger("Start");

		yield return new WaitForSeconds(Duration);
	}
}
