using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel {Master, Sfx, Music};

    public float masterVolumPercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    private AudioSource sfx2DSource;
    private AudioSource[] musicSources;
    private int activeMusciSourceIndex;

    public static AudioManager instance;

    private Transform audioListener;
    private Transform playerT;

    private SoundLibrary library;

    private void Awake(){
        if (instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++){
                GameObject newMusciSource = new GameObject("Music source" + (i + 1));
                musicSources[i] = newMusciSource.AddComponent<AudioSource>();
                newMusciSource.transform.parent = transform;
            }
            GameObject newSfx2Dsource = new GameObject("2D sfx source");
            sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
            newSfx2Dsource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;
            if (playerT == null){
                playerT = this.transform;
            }

            masterVolumPercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
            PlayerPrefs.Save();
        }
    }
    void Start () {
		
	}

    void Update () {
        if (playerT != null){
            audioListener.position = playerT.position;
        }
	}

    public void SetVolume(float volumePercent, AudioChannel channel){
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumPercent = volumePercent; 
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }
        musicSources[0].volume = musicVolumePercent * masterVolumPercent;
        musicSources[1].volume = musicVolumePercent * masterVolumPercent;

        PlayerPrefs.SetFloat("master vol", masterVolumPercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);

    }

    public void PlaySound(string soundName, Vector3 pos){
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    public void PlaySound(AudioClip clip, Vector3 pos){
        if (clip != null){
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumPercent);
        }
    }

    public void PlaySound2D(string soundName){
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumPercent);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f){ 
        activeMusciSourceIndex = 1 - activeMusciSourceIndex;
        musicSources[activeMusciSourceIndex].clip = clip;
        musicSources[activeMusciSourceIndex].Play();
        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
    }

    IEnumerator AnimateMusicCrossFade(float duration){
        float percent = 0;

        while(percent < 1){
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusciSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumPercent, percent);
            musicSources[1-activeMusciSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumPercent, 0, percent);
            yield return null;
        }
    }
}
