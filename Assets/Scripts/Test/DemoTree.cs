using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTree : Sequence {
    public Sprite[] animationFrames; // Animation Frames that looped through in a linear fashion.
    public Vector3 startSize = new Vector3(1, 1, 1); // Scale to start your sprite
    public Vector3 endSize = new Vector3(3, 5, 3); // Scale of your sprite at the end of the sequence.
    float distCovered = 0; // Amount of progress between 0 and 1.
    public SpriteRenderer renderer;
    private GameObject camera;
    // Use this for initialization

    public override void Start() {
        camera = Camera.main.gameObject;
        renderer = GetComponent<SpriteRenderer>(); // The Sprite Renderer that visualises the sprite / animation
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && growth == false) { // Iniatates growth when space is pressed.
            growth = true;
            Invoke("StopGrowth", 1f); // Stops Growth after 1 second.
        }

        if (growth) {
            distCovered += Time.deltaTime * speed; // Adds growth to the sequence that is divided by speed seconds (I.E. With 0.5 speed you need to have your hand in the growth area for two seconds. with 0.02 it's 50 seconds ( 0.02 * 50 = 1))
            if (distCovered >= 1) { // Small hack to prevent sprites from overflowing.
                distCovered = 0.999f;
            }
<<<<<<< HEAD
        }
=======
        } 
>>>>>>> 03c8648271183da86f7399c9a055b0a31f82b1ee
        else {
            distCovered -= Time.deltaTime * speed;
            if (distCovered < 0.000) { // Small hack to prevent sprites from overflowing.
                distCovered = 0.001f;
            }
        }

        renderer.sprite = animationFrames[Mathf.FloorToInt(animationFrames.Length * distCovered)]; // Change sprite based on point in sequence. (I.E. With 5 sprites it changes to the next one every 1 / 5 = 0.2 so it will change sprites at 0.2 , 0.4, 0.6, 0.8 etc in a linear fashion.

<<<<<<< HEAD
        camera.GetComponent<Camera>().fieldOfView = 20 + (distCovered * 35);
=======
        camera.GetComponent<Camera>().fieldOfView = zoom + (distCovered * zoomScale);
>>>>>>> 03c8648271183da86f7399c9a055b0a31f82b1ee
        float fracJourney = distCovered; // Current Point in the sequence.
        transform.localScale = Vector3.Lerp(startSize, endSize, fracJourney); // Change Size
    }

    void StopGrowth() {
        growth = false;
    }
}
