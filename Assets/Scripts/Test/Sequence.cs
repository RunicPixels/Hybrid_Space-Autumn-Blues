using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {
    public float startTime;
    public float journeyLength;
    public float speed = 1.0F;
    public bool growth = false;
    // Use this for initialization
    public virtual void Start () {
        startTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Grow() {

    }
}
