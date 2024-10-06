using System.Collections;
using System.Collections.Generic;
using Night;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	public Unit Unit => GetComponentInParent<Unit>();
	
	public void ZAttack()
	{
		Unit.ZAttack();
	}
}
