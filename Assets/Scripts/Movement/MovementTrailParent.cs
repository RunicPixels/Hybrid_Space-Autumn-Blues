using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrailParent : MonoBehaviour {

	public MovementTrail leftTrail, rightTrail;

    public ParticleSystem leftTargetParticles, rightTargetParticles;

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

    public void DisableParticles()
    {
        leftTrail.DisableParticles();
        rightTrail.DisableParticles();

        var main = leftTargetParticles.main;
        main.startLifetime = 0;
        main = rightTargetParticles.main;
        main.startLifetime = 0;
    }
}
