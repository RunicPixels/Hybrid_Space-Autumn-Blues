using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrailParent : MonoBehaviour {

	public MovementTrail leftTrail, rightTrail;

	public void Pause()
	{
		leftTrail.paused = true;
		rightTrail.paused = true;
	}

	public void UnPause()
	{
		leftTrail.paused = false;
		rightTrail.paused = false;
	}
}
