using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTree : Sequence {
    public Sprite[] animationFrames;
    public Vector3 startSize = new Vector3(1 ,1 ,1 );
    public Vector3 endSize = new Vector3(3, 5, 3);
    float distCovered = 0;
    public SpriteRenderer renderer;
    // Use this for initialization

    public override void Start() {
        renderer = GetComponent<SpriteRenderer>();
        base.Start();
    }
	


	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space) && growth == false) {
            growth = true;
            Invoke("StopGrowth", 1f);
        }

        if (growth) {
            distCovered += Time.deltaTime * speed;
            if(distCovered >= 1) {
                distCovered = 0.999f;
            }
            renderer.sprite = animationFrames[Mathf.FloorToInt(animationFrames.Length * distCovered)];

        }
        float fracJourney = distCovered;
        transform.localScale = Vector3.Lerp(startSize, endSize, fracJourney);
	}

    void StopGrowth() {
        growth = false;
    }
}
