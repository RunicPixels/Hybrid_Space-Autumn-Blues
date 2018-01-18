using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneOrb : MonoBehaviour {

    public StartSceneOrb otherOrb;
    public bool active = false;
    private bool triggered = false

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(otherOrb != null)
        {
            if ((active && otherOrb.active) && !triggered)
            {
                GameObject.Find("DontRemoveItStartsBreathing").GetComponent<StartBreading>().startSceneOne = true;
                triggered = false;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hand")
        {
            active = true;
        }
    }
}
