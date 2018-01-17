using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip StartScene;
    public AudioClip Scene1;
    public AudioClip Scene2;


    private string sceneName;

    void Start () {
        OnLevelWasLoaded(0);
    }
	
	void Update () {
		
	}

    private void OnLevelWasLoaded(int level){
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != sceneName){
            sceneName = newSceneName;
            Invoke("PlayMusic", 0.2f);
        }
    }

    void PlayMusic(){
        AudioClip clipToPlay = null;
        if (sceneName == "StartScene"){
            clipToPlay = StartScene;
        }
        else if (sceneName == "Main Scene 1 - Tree")
        {
            clipToPlay = Scene1;
        }
        else if (sceneName == "Main Scene 2 - Mountains")
        {
            clipToPlay = Scene2;
        }

        if (clipToPlay != null){
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }
}
