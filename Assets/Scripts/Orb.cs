using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    public MovementTrail trail;
    public float maxDistanceDelta;

    public Gradient activeColor;
    public Gradient inactiveColor;
    private ParticleSystem.MainModule main;
    private ParticleSystem system;

    // Use this for initialization
    void Start () {
        system = gameObject.GetComponent<ParticleSystem>();
        main = system.main;
    }
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, trail.NextTrailSection(transform.position), maxDistanceDelta);
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.tag == "Hand")
        {
            MovementManager.instance.UnPause();
            MovementManager.instance.movementActive = true;
            main.startColor = activeColor;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if (collision.tag == "Hand")
        {
            MovementManager.instance.movementActive = false;
            main.startColor = inactiveColor;
        }
    }
}
