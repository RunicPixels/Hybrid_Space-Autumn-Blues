using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    private ParticleSystem system;
    public Gradient activeColor;
    public Gradient inactiveColor;
    private ParticleSystem.MainModule main;
    // Use this for initialization
    void Start () {
        system = gameObject.GetComponent<ParticleSystem>();
        main = system.main;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D( Collider2D collision ) {
        if (collision.tag == "Hand") {
            MovementManager.instance.UnPause();
            MovementManager.instance.movementActive = true;
            main.startColor = activeColor;
        }
    }
    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.tag == "Hand") {
            MovementManager.instance.movementActive = false;
            main.startColor = inactiveColor;
        }
    }
}
