using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrailParent : MonoBehaviour {

	public MovementTrail leftTrail, rightTrail;

    public ParticleSystem leftTargetParticles, rightTargetParticles;

    private float orbLifetime = 0.2f;

	public void Pause()
	{
        Debug.Log("pauseparent");
		leftTrail.paused = true;
		rightTrail.paused = true;
	}

	public void UnPause()
	{
		leftTrail.paused = false;
		rightTrail.paused = false;
        Debug.Log("unpause");
	}

    public void ResetTrails()
    {
        leftTrail.ResetTrail();
        rightTrail.ResetTrail();
    }

    public bool BothTargetsActive
    {
        get { return leftTrail.targetActive && rightTrail.targetActive; }
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

    public void EnableParticles()
    {
        leftTrail.EnableParticles();
        rightTrail.EnableParticles();

        var main = leftTargetParticles.main;
        main.startLifetime = orbLifetime;
        main = rightTargetParticles.main;
        main.startLifetime = orbLifetime;
    }
}
