using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public float distanceFromCamera;
    public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;
        transform.position = cam.ScreenToWorldPoint(mousePosition);
    }
}
