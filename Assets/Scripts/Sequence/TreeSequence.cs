using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSequence : Sequence {

    public float finalScale;
    public Vector3 finalCameraPosition;
    public Sprite[] textures;
    public Transform faseObject;
    public SpriteRenderer spriteRenderer;
    private Vector3 finalScaleVector, startingScaleVector, startingCameraVector;
    private float scaleLength;


    // Use this for initialization
    public override void Start () {
        base.Start();

        //Finish scaling by the 8th sprite
        scaleLength = (sequenceLength / textures.Length) * 8;

        startingScaleVector = faseObject.transform.localScale;
        finalScaleVector = new Vector3(finalScale, finalScale, finalScale);

        startingCameraVector = mainCamera.transform.position;

        currentTime = 0f;
	}

    public override void Progress()
    {
        base.Progress();

        faseObject.transform.localScale = Vector3.Lerp(startingScaleVector, finalScaleVector, currentTime / scaleLength);
        mainCamera.transform.position = Vector3.Lerp(startingCameraVector, finalCameraPosition, progress);

        int textureIndex = (int)Mathf.Floor((progress) / (1f/(float)textures.Length)) - 1;
        if(textureIndex >= 0)
            spriteRenderer.sprite = textures[textureIndex];
    }

    public float CurrentTime
    {
        get { return currentTime; }
    }
}
