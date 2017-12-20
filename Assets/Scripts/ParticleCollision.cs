using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    public Sequence sequence;
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

    private void OnTriggerStay2D( Collider2D collision ) {
        //Debug.Log("yes");
        if (collision.tag == "Hand") {
            sequence.growth = true;
            //Debug.Log("yes");
            main.startColor = activeColor;
        }
    }
    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.tag == "Hand") {
            sequence.growth = false;
            main.startColor = inactiveColor;
        }
    }
}
