using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSequence : MonoBehaviour {

    [HideInInspector()]
    public bool active = false;
    public float sequenceLength, finalScale;
    public Vector3 finalCameraPosition;
    public Sprite[] textures;
    public Camera perspectiveCamera;
    public Transform faseObject;
    public SpriteRenderer spriteRenderer;
    private Vector3 finalScaleVector, startingScaleVector, startingCameraVector;

    private int faseIndex = 0;
    private float timer, currentTime, scaleLength;


    // Use this for initialization
    void Start () {
        timer = sequenceLength / textures.Length;

        scaleLength = timer * 8;

        startingScaleVector = faseObject.transform.localScale;
        finalScaleVector = new Vector3(finalScale, finalScale, finalScale);

        startingCameraVector = perspectiveCamera.transform.position;

        currentTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if((active || Input.GetKey(KeyCode.Space)) && faseIndex < textures.Length)
        {
            faseObject.transform.localScale = Vector3.Lerp(startingScaleVector, finalScaleVector, currentTime / scaleLength);
            perspectiveCamera.transform.position = Vector3.Lerp(startingCameraVector, finalCameraPosition, currentTime / sequenceLength);
            currentTime += Time.deltaTime;

            if(timer <= 0 && faseIndex < textures.Length - 1)
            {
                faseIndex++;
                spriteRenderer.sprite = textures[faseIndex];
                timer = sequenceLength / textures.Length;
            }
            else
                timer -= Time.deltaTime;
        }
	}
}
