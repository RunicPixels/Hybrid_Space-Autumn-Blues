using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {
    public float sequenceLength;
    // Amount of progress between 0 and 1.   
    [HideInInspector()]
    public float progress = 0;
    [HideInInspector()]                                                                       
    public bool active = true;
    public Camera mainCamera;

    protected float currentTime = 0f;

    // Use this for initialization

    public virtual void Start() {

    }
	
	// Update is called once per frame
	public virtual void Update () {
        if(active || Input.GetKey(KeyCode.Space))
        {
            Progress();
        }

    }

    public virtual void Progress() 
    {
        currentTime += Time.deltaTime;
        progress = Mathf.Min(1f, currentTime / sequenceLength);

    }

}
