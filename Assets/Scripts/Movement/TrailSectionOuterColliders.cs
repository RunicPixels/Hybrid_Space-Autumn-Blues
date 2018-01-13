using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSectionOuterColliders : MonoBehaviour {

    public float width;

    private MovementTrail movementTrail;

	// Use this for initialization
	void Start () {
        movementTrail = transform.parent.parent.GetComponent<MovementTrail>();

        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        float distanceToCenter = (width / 2f) + (colliders[0].size.x / 2f);
        colliders[0].offset = new Vector2(0, distanceToCenter);
        colliders[1].offset = new Vector2(0, -distanceToCenter);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "Hand")
            // movementTrail.Reset();
    }
}
