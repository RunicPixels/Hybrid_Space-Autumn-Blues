using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBreading : MonoBehaviour {


    public Texture2D fadeOutTexture;
    public float fadeSpeed = 1.5f;
    int drawDepth = -1000; 
    float alpha = 1f;
    int fadeDir = -1;
    public int curretScene = 0;

    public List<Animator> allAniInScene = new List<Animator>();
    public float startCoundown = 4;
    public float currentCoundown;
    public Texture2D color;
     
    void Awake(){
        DontDestroyOnLoad(this);
    }

    private void Update(){
        if (Input.GetKeyDown("b")){
            if (SceneManager.GetActiveScene().name != "StartScene"){
                curretScene = 0;
                StartCoroutine(Fade());
            }
        }

        if (Input.GetKeyDown("1")){
            curretScene = 2;
            StartCoroutine(Fade());
        }
        if (Input.GetKeyDown("2")){
            curretScene = 3;
            StartCoroutine(Fade());
        }
        if (currentCoundown > 0){
            currentCoundown = currentCoundown - Time.deltaTime;
        }
        else{
            StartCoroutine(StartAni(true));
        }
    }

    IEnumerator Fade(){
        int c = curretScene;
        float fadeTime = BeginFade(1); 
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(c);
    }

    void StartTimer(){
        currentCoundown = startCoundown;
    }

    private void OnGUI(){
        if (currentCoundown > 0){
            GUI.DrawTexture(new Rect(0, Screen.height - 10, Screen.width*(currentCoundown/startCoundown), 10), color);
        }

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
        StartTimer();
        StartCoroutine(StartAni(false));
    }

    IEnumerator StartAni(bool b){
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < allAniInScene.Count; i++){
            allAniInScene[i].enabled = b;
        }

        /*
        object[] an = FindObjectsOfType(typeof(Animator));
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < an.Length; i++){
            Debug.Log(an.Length);
            Animator a = an[i] as Animator;
            Debug.Log(a);
            a.enabled = b;
        }*/
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
