using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Sprite[] textures;

    private int faseIndex = -1;
    private Transform faseObject;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        faseObject = transform.GetChild(0).transform;
        spriteRenderer = faseObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grow()
    {
        faseIndex++;
        spriteRenderer.sprite = textures[faseIndex];
    }
}
