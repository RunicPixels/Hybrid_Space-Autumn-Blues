using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public static MovementManager instance;

	[HideInInspector()]
	public bool movementActive = false;

    public Sequence sequence;
	public GameObject[] movements;

	private MovementTrailParent[] parents;
    private int movementIndex = 0, repetitions = -1;
	private GameObject currentMovement;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		currentMovement = transform.GetChild(0).gameObject;

		parents = new MovementTrailParent[movements.Length];
		for(int i = 0; i < movements.Length; i++)
			parents[i] = movements[i].GetComponent<MovementTrailParent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
			NextMovement();

        if (Input.GetKeyDown(KeyCode.R))
            Pause();
        if (Input.GetKeyDown(KeyCode.T))
            UnPause();
        
        movementActive = parents[movementIndex].BothTargetsActive;
        if (movementActive)
			sequence.active = true;
		else
			sequence.active = false;
	}

	public void NextMovement()
	{
        StartCoroutine(NextMovementCoRoutine());
    }

    private IEnumerator NextMovementCoRoutine()
    {
        parents[movementIndex].DisableParticles();
        yield return new WaitForSeconds(1);
        
        movements[movementIndex].SetActive(false);

        movementIndex++;
        if (movementIndex == movements.Length)
            movementIndex = 0;

        parents[movementIndex].EnableParticles();
        parents[movementIndex].ResetTrails();
        movements[movementIndex].SetActive(true);
    }

    public void DisableCurrentMovement()
    {
        StartCoroutine(DisableCurrentMovementCoRoutine());
    }

    public IEnumerator DisableCurrentMovementCoRoutine()
    {
        parents[movementIndex].DisableParticles();
        yield return new WaitForSeconds(1);

        movements[movementIndex].SetActive(false);
    }

    public void UnPause()
	{
		parents[movementIndex].UnPause();
	}

	public void Pause()
	{
		parents[movementIndex].Pause();
	}
}
