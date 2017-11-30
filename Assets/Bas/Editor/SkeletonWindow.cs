using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(SkeletonWindow))]
public class SkeletonWindow : EditorWindow{

    [MenuItem("Tools/SkeletonWindow")]
    public static void skeletonWindow(){
        GetWindow<SkeletonWindow>("SkeletonWindow");
    }

    [System.Serializable]
    public class Possess {
        public bool open;
        // public Color color;
        //public GameObject prefab;
        public Texture pos;
        public string testt;
        public List<Texture> p = new List<Texture>();
    }

    public static string AssetFilter = "t:png";
    Vector3 addVec;
    string namePNG;
    Texture texture;
    public List<Possess> pose = new List<Possess>();
    public List<Texture> pose1 = new List<Texture>();
    Test other;

    public void OnEnable(){
        GameObject go = GameObject.Find("GameObject");
        other = (Test)go.GetComponent(typeof(Test));
    }

    void Refresh(){
        AssetDatabase.Refresh();
        string[] collection = AssetDatabase.FindAssets(AssetFilter, null);

        List<GameObject> tt = new List<GameObject>();
        foreach (var item in collection){
            //tt.Add(item);
        }

    }

    void OnGUI(){
        GUILayout.Label("Generate Level", EditorStyles.boldLabel);

        addVec = (Vector3)EditorGUI.Vector3Field(new Rect(5, 25, position.width - 50, 60), "Add a Texture:", addVec);
        namePNG = (string)EditorGUI.TextField(new Rect(5, 65, position.width - 50, 16), "Add a Texture:", namePNG);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("pose");
        EditorGUI.PropertyField(new Rect(5, 85, position.width - 50, 60), stringsProperty, true);
        so.ApplyModifiedProperties();

        SerializedObject so1 = new SerializedObject(target);
        SerializedProperty stringsProperty1 = so1.FindProperty("pose1");
        EditorGUI.PropertyField(new Rect(5, 100, position.width - 50, 60), stringsProperty1, true);
        so1.ApplyModifiedProperties();

        pose1 = other.PoseImage;

        //texture = (Texture2D)EditorGUI.ObjectField(new Rect(5, 20, position.width - 50, 60), "Add a Texture:", texture, typeof(Texture2D), allowSceneObjects: true);

        if (GUI.Button(new Rect(0, position.height - 50, position.width, 25), " Refresh ")){
            Refresh();
        }

        if (GUI.Button(new Rect(0, position.height - 25, position.width, 25)," start ")){
            //other.SavePose(addVec, namePNG);
            Texture2D addToList = Resources.Load(namePNG) as Texture2D;
            pose1.Add(addToList);
        }
    }
}

