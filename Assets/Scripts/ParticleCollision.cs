using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    public Sequence sequence;
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

    private void OnTriggerEnter2D( Collider2D collision ) {
        var main = system.main;
        Debug.Log("yes");
        if (collision.tag == "Hand") {
            sequence.growth = true;
            Debug.Log("yes");
            main.startColor = activeColor;
        }
    }
    private void OnTriggerExit2D( Collider2D collision ) {
        var main = system.main;
        if (collision.tag == "Hand") {
            sequence.growth = false;
            main.startColor = inactiveColor;
        }
    }
}
