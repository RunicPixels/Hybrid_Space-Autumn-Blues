using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToCameraRotator : MonoBehaviour {
    private GameObject camera;
	// Use this for initialization
	void Start () {
        camera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(-camera.transform.forward);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

    }
}
