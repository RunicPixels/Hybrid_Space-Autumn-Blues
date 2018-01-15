using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTree : Sequence {
    public Sprite[] animationFrames; // Animation Frames that looped through in a linear fashion.
    public Vector3 startSize = new Vector3(1, 1, 1); // Scale to start your sprite
    public Vector3 endSize = new Vector3(3, 5, 3); // Scale of your sprite at the end of the sequence.
    public SpriteRenderer renderer;
    // Use this for initialization

    public override void Start() {
        renderer = GetComponent<SpriteRenderer>(); // The Sprite Renderer that visualises the sprite / animation
        base.Start();
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
        transform.localScale = Vector3.Lerp(startSize, endSize, distCovered); // Change Size
    }

    void StopGrowth() {
        growth = false;
    }

    public override void Grow() {
        base.Grow();
        renderer.sprite = animationFrames[Mathf.FloorToInt(animationFrames.Length * distCovered)]; // Change sprite based on point in sequence. (I.E. With 5 sprites it changes to the next one every 1 / 5 = 0.2 so it will change sprites at 0.2 , 0.4, 0.6, 0.8 etc in a linear fashion.
    }
}
