using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTree : Sequence {
    public Vector3 startSize = new Vector3(1 ,1 ,1 );
    public Vector3 endSize = new Vector3(3, 5, 3);
    float distCovered = 0;
    // Use this for initialization

    public override void Start() {
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
        }
        float fracJourney = distCovered;
        transform.localScale = Vector3.Lerp(startSize, endSize, fracJourney);
	}

    void StopGrowth() {
        growth = false;
    }
}
