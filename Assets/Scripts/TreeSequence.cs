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
    private float currentTime, scaleLength;


    // Use this for initialization
    void Start () {
        //Finish scaling by the 8th sprite
        scaleLength = (sequenceLength / textures.Length) * 8;

        startingScaleVector = faseObject.transform.localScale;
        finalScaleVector = new Vector3(finalScale, finalScale, finalScale);

        startingCameraVector = perspectiveCamera.transform.position;

        currentTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if((active || Input.GetKey(KeyCode.Space)) && currentTime <= sequenceLength)
        {
            currentTime += Time.deltaTime;

            SetProgression(currentTime);
        }
	}

    public void SetProgression(float time)
    {
        currentTime = time;

        faseObject.transform.localScale = Vector3.Lerp(startingScaleVector, finalScaleVector, currentTime / scaleLength);
        perspectiveCamera.transform.position = Vector3.Lerp(startingCameraVector, finalCameraPosition, currentTime / sequenceLength);

        int textureIndex = (int)Mathf.Floor((currentTime / sequenceLength) / (1f/(float)textures.Length)) - 1;
        if(textureIndex >= 0)
            spriteRenderer.sprite = textures[textureIndex];
    }

    public float CurrentTime
    {
        get { return currentTime; }
    }
}
