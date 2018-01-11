using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBreading : MonoBehaviour {

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;
    int drawDepth = -1000; 
    float alpha = 1f;
    int fadeDir = -1;
    public int curretScene = 0;

    void Awake(){
        DontDestroyOnLoad(this);
    }

    private void Update(){
        if (Input.GetKeyDown("p")){
            StartCoroutine(Fade());
         }
    }

    IEnumerator Fade(){
        int c = curretScene;
        curretScene = SceneManager.GetActiveScene().buildIndex;
        float fadeTime = BeginFade(1); 
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(c);
    }

    private void OnGUI(){
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);

    }

    public float BeginFade(int direction){
        fadeDir = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded(int level){
        BeginFade(-1);

    }


}
/*
    public  bool active = true;
    List<MeshRenderer> allChildMat = new List<MeshRenderer>();
    private float fadePerSecond = 2.5f;

    void Start () {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i){
            if (transform.GetChild(i).GetComponent<MeshRenderer>() != null){
                allChildMat.Add(transform.GetChild(i).GetComponent<MeshRenderer>());

            }
        }
    }

    void Update () {
        if (Input.GetKeyDown("p")){
            active = !active;
        }

        if (active){
            //transform.GetChild(0).gameObject.SetActive(active);
            Fade();
        }
	}

    void Fade(){

        for (int i = 0; i < allChildMat.Count; ++i){
            //allChildMat[i].material.color = Color.red;
            var color = allChildMat[i].material.color;

            allChildMat[i].material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            //print("For loop: " + transform.GetChild(i));

        }


    }
}*/
