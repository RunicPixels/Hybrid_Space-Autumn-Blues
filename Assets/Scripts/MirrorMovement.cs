using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour {

    public Transform toMirror;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = toMirror.position;
        transform.position = new Vector2(-position.x, position.y);
	}
}
