using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMountain : Sequence {                             
    public Vector3 startSize = new Vector3(1 ,1 ,1 );                                                   // Scale to start your sprite
    public Vector3 endSize = new Vector3(3, 5, 3);                                                      // Scale of your sprite at the end of the sequence.
    public float distCovered = 0;                                                                       // Amount of progress between 0 and 1.
    public SpriteRenderer[] renderers;                                                                  // The Sprite Renderer that visualises the sprite / animation
    public MountainAnimations[] mountainAnimations;

    public Gradient skyGradient;
    private Skybox skybox;
    private Material skyMaterial;
    public Color skyColour;


    public override void Start() {
        base.Start();
        skybox = Camera.main.GetComponent<Skybox>();
        skyMaterial = skybox.material;
    }


	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space) && growth == false) {                                        // Iniatates growth when space is pressed.
            growth = true;
            Invoke("StopGrowth", 1f);                                                                   // Stops Growth after 1 second.
        }

        if (growth) {
            distCovered += Time.deltaTime * speed;                                                       // Adds growth to the sequence that is divided by speed seconds (I.E. With 0.5 speed you need to have your hand in the growth area for two seconds. with 0.02 it's 50 seconds ( 0.02 * 50 = 1))
            if(distCovered >= 1) {                                                                       // Small hack to prevent sprites from overflowing.
                distCovered = 0.999f;
            }
            for(int i = 0; i< renderers.Length; i++) {
            renderers[i].sprite = mountainAnimations[i].animationFrames[Mathf.FloorToInt(mountainAnimations[i].animationFrames.Length * distCovered)];  // Change sprite based on point in sequence. (I.E. With 5 sprites it changes to the next one every 1 / 5 = 0.2 so it will change sprites at 0.2 , 0.4, 0.6, 0.8 etc in a linear fashion.
            }
        }
        float fracJourney = distCovered;                                                                // Current Point in the sequence.
        transform.localScale = Vector3.Lerp(startSize, endSize, fracJourney);                           // Change Size
        skyColour = skyGradient.Evaluate(fracJourney);
        skyMaterial.SetColor("_Tint", skyColour);
	}

    void StopGrowth() {
        growth = false;
    }
}
