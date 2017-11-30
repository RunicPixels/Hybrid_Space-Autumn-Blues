using UnityEngine;

public class ParticleLight : MonoBehaviour {
    private ParticleSystem system;
    private Light lightS;
	// Use this for initialization
	void Start () {
        system = gameObject.GetComponent<ParticleSystem>();
        lightS = gameObject.GetComponent<Light>();
        lightS.color = system.main.startColor.color;

    }
	
	// Update is called once per frame
	void Update () {
        

	}
}
