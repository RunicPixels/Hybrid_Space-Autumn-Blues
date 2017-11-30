using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneTool : Editor{

    [MenuItem("Tools/Scene/TestKinectScene")]
    public static void TestKinectScene(){
        OpenScene("Kinect Testing");
    }
    [MenuItem("Tools/Scene/KinectView")]
    public static void KinectView(){
        OpenScene("MainScene");
    }
    [MenuItem("Tools/Scene/GreenScreen")]
    public static void GreenScreen(){
        OpenScene("MainScene1");
    }

    static void OpenScene(string name){
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            if (name == "MainScene"){
                EditorSceneManager.OpenScene("Assets/Kinect Test/KinectView/" + name + ".unity");
            }else if(name == "MainScene1"){
                EditorSceneManager.OpenScene("Assets/Kinect Test/GreenScreen/" + name + ".unity");
            }
            else{
                EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity");
            }
        }
    }
}

