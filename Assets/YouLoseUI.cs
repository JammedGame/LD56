using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;

public class YouLoseUI : MonoBehaviour
{
	public float Duration;	
	public Animator Animator;	
	private AudioSource audioSource;
	
	void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

	public IEnumerator Animate(NightBattleContext battleContext)
	{
		gameObject.SetActive(true);
        if (audioSource != null)
        {
            audioSource.Play();
        }
		Animator.SetTrigger("Start");

		yield return new WaitForSeconds(Duration);
	}
}
