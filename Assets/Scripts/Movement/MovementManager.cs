using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public static MovementManager instance;

	[HideInInspector()]
	public bool movementActive = false;
    // public TreeSequence sequence;
    public Sequence sequence;
	public GameObject[] movements;

	private MovementTrailParent[] parents;
    
	private int movementIndex = 0;
	private GameObject currentMovement;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		//currentMovement = transform.GetChild(0).gameObject;

		//parents = new MovementTrailParent[movements.Length];
		//for(int i = 0; i < movements.Length; i++)
		//	parents[i] = movements[i].GetComponent<MovementTrailParent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
			NextMovement();

		if(movementActive)
			sequence.active = true;
		else
			sequence.active = false;
	}
	public void NextMovement()
	{
		//movementIndex++;
		//Destroy(currentMovement.gameObject);
		//currentMovement = (GameObject)Instantiate(movements[movementIndex], Vector3.zero, Quaternion.identity, this.transform);
	}

	public void UnPause()
	{
		//parents[movementIndex].UnPause();
	}

	public void Pause()
	{
		//parents[movementIndex].Pause();
	}
}
