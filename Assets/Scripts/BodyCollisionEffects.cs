using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollisionEffects : MonoBehaviour {
    private ParticleSystem system;
    private ParticleSystem.ShapeModule shape;
    private float baseShape;
	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
        shape = system.shape;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D( Collider2D collision ) {
        if(collision.tag == "Hand" && tag == "Hand") {
            Debug.Log("Collision between hands.");
            transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }
}
