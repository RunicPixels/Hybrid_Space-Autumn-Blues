using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour {

	public MovementTrail trail;
	public float maxDistanceDelta;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, trail.NextTrailSection(transform.position), maxDistanceDelta);
	}
}
