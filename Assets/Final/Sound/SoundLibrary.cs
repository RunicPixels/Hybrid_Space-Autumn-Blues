using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour {

    public SoundGroup[] soundGroups;
    private Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

    private void Awake(){
        foreach (SoundGroup soundGroup in soundGroups){
            groupDictionary.Add(soundGroup.GroupID, soundGroup.group);
        }
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    public AudioClip GetClipFromName(string name){
        if (groupDictionary.ContainsKey(name)){
            AudioClip[] sounds = groupDictionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup{
        public string GroupID;
        public AudioClip[] group;
    }
}
