using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    public TreeSequence sequence;
    private ParticleSystem system;
    public Gradient activeColor;
    public Gradient inactiveColor;
    // Use this for initialization
    void Start () {
        system = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

<<<<<<< HEAD
    private void OnTriggerStay2D( Collider2D collision ) {
=======
    private void OnTriggerEnter2D( Collider2D collision ) {
        var main = system.main;
        //Debug.Log("yes");
>>>>>>> 5ac191d988d3e8d9b0c0ad4256bf790fe95f836e
        if (collision.tag == "Hand") {
            sequence.active = true;
            main.startColor = activeColor;
        }
    }
    private void OnTriggerExit2D( Collider2D collision ) {
        var main = system.main;
        if (collision.tag == "Hand") {
            sequence.active = false;
            main.startColor = inactiveColor;
        }
    }
}
