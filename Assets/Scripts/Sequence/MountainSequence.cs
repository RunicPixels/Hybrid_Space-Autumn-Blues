using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSequence : Sequence {                             
    public Vector3 startSize = new Vector3(1 ,1 ,1 );                                                   // Scale to start your sprite
    public Vector3 endSize = new Vector3(3, 5, 3);                                                      // Scale of your sprite at the end of the sequence.
    public SpriteRenderer[] renderers;                                                                  // The Sprite Renderer that visualises the sprite / animation
    public MountainAnimations[] mountainAnimations;
    public GameObject[] stars;

    

    public Gradient skyGradient;
    private Skybox skybox;
    private Material skyMaterial;
    public Color skyColour;

    private float camBaseZPos;
    public float camZPosModifier = 10;
    private float camXRot;
    public float camXRotModifier = 5;

    public override void Start() {
        base.Start();

        skybox = Camera.main.GetComponent<Skybox>();
        skyMaterial = skybox.material;
        camBaseZPos = mainCamera.transform.position.z;
        camXRot = mainCamera.transform.rotation.eulerAngles.x;
    }


	// Update is called once per frame
	public override void Update () {
        base.Update();
        transform.localScale = Vector3.Lerp(startSize, endSize, progress); // Change Size 
        skyColour = skyGradient.Evaluate(progress);
        skyMaterial.SetColor("_Tint", skyColour);
	}

    public override void Progress() {
        base.Progress();

        int starIndex = Mathf.FloorToInt(stars.Length * progress);
        if(starIndex < stars.Length)
            stars[starIndex].SetActive(true);

        for (int i = 0; i < renderers.Length; i++) {
            int index = Mathf.FloorToInt(mountainAnimations[i].animationFrames.Length * progress);
            if(index < mountainAnimations[i].animationFrames.Length)
                renderers[i].sprite = mountainAnimations[i].animationFrames[index];  // Change sprite based on point in sequence. (I.E. With 5 sprites it changes to the next one every 1 / 5 = 0.2 so it will change sprites at 0.2 , 0.4, 0.6, 0.8 etc in a linear fashion.
        }
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, camBaseZPos + (camZPosModifier * progress));

        float angle = (progress * camXRotModifier) + camXRot;
        mainCamera.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
    }

}
