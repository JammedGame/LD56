using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;

public class YouLoseUI : MonoBehaviour
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
