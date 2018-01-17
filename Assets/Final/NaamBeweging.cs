using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NaamBeweging : MonoBehaviour {

    private Text naam;
    public GameObject bewegingen;

    void Start () {
        naam = transform.GetChild(0).GetComponent<Text>();
	}


    void Update () {
        if (SceneManager.GetSceneByBuildIndex(3).isLoaded){
            naam.enabled = false;
        }
        else{
            naam.enabled = true;
        }

        for (int i = 0; i < bewegingen.transform.childCount; i++){
            if (bewegingen.transform.GetChild(i).gameObject.activeSelf == true){
                naam.text = bewegingen.transform.GetChild(i).name;
            }
        }
    }
}
