using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSection : MonoBehaviour {

    public Color normalColor, highlightColor;
    public TrailSectionOuterColliders outerColliders;

    [HideInInspector()]
    public int sectionNumber;
    [HideInInspector()]
    public Vector2 position1, position2;
    [HideInInspector()]
    public float width, particleLifetime;
    [HideInInspector()]
    public bool highlighted = false;

    private ParticleSystem.MainModule particleMain;
    private BoxCollider2D innerCollider, outerCollider;
    private MovementTrail parent;

	// Use this for initialization
	void Start () {
        /* Component initialization */
        innerCollider = GetComponent<BoxCollider2D>();
        particleMain = transform.GetChild(0).GetComponent<ParticleSystem>().main;
        parent = transform.parent.GetComponent<MovementTrail>();
        outerColliders.width = width;
        
        particleMain.startLifetime = particleLifetime / 50f;
        
        /* Set collider size to match the length of this section and width specified by MovementTrail */
        float sectionLength = Vector3.Distance(position1, position2);
        innerCollider.size = new Vector3(sectionLength, width, 1f);
        
        Vector2 midPoint = (position1 + position2) / 2;
        transform.position = midPoint;

        /* Calculate rotation of this section */
        float angle = Mathf.Abs(position1.y - position2.y) / Mathf.Abs(position1.x - position2.x);
        if ((position1.y < position2.y && position1.x > position2.x) || (position2.y < position1.y && position2.x > position1.x))
            angle *= -1;
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        particleMain.startColor = normalColor;
        highlighted = false;
    }

    public void Highlight()
    {
        particleMain.startColor = highlightColor;
        highlighted = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      //  if (other.tag == "Hand")
        //    parent.ActivateTrailPart(sectionNumber);
    }
}
