using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {
    public float startTime;
    public float journeyLength;
    public float speed = 1.0F;
    public float distCovered = 0;                                                                       // Amount of progress between 0 and 1.
    public bool growth = false;
    public Camera mainCamera;
    private float zoom;
    public float zoomScale = 20;

    // Use this for initialization

    public virtual void Start() {
        mainCamera = Camera.main;

        startTime = Time.time;
        zoom = mainCamera.GetComponent<Camera>().fieldOfView;

    }
	
	// Update is called once per frame
	public virtual void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && growth == false) {                                        // Iniatates growth when space is pressed.
            growth = true;
            Invoke("StopGrowth", 100f);                                                                   // Stops Growth after 1 second.
        }
        if (growth) {
            Grow();
        }

        mainCamera.GetComponent<Camera>().fieldOfView = zoom + (distCovered * zoomScale);
    }

    public virtual void Grow() {
        distCovered += Time.deltaTime * speed;                                                        // Adds growth to the sequence that is divided by speed seconds (I.E. With 0.5 speed you need to have your hand in the growth area for two seconds. with 0.02 it's 50 seconds ( 0.02 * 50 = 1))
        if (distCovered >= 1) {                                                                       // Small hack to prevent sprites from overflowing.
            distCovered = 0.999f;
        }
    }

    void StopGrowth() {
        growth = false;
    }

}
