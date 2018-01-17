using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip mainTheme;
    public AudioClip menuTheme;

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
            clipToPlay = menuTheme;
        }
        else if (sceneName == "Main Scene 1 - Tree"){
            clipToPlay = mainTheme;
        }

        if (clipToPlay != null){
            AudioManager.instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }
}
