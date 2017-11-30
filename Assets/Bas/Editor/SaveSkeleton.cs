using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Test))]
public class SaveSkeleton : Editor {

    Vector3 addVec;
    int delPos;
    string namePNG;


    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Test myScript = (Test)target;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TestAura"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("BoneMaterial"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("BodySourceManager"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Pose"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("PoseImage"), true);

        EditorGUILayout.Space();
        GUILayout.Label("Add or Remove pose!");

        EditorGUILayout.BeginHorizontal();
        delPos = EditorGUILayout.IntField("Delete Pose", delPos);
        if (GUILayout.Button(" - ", GUILayout.Width(50))) {
            if (myScript.Pose.Count != 0 && myScript.Pose.Count >= delPos && myScript.Pose.Count >= delPos) {
                myScript.DeleteSkeleton(delPos);
            }
        }
        EditorGUILayout.EndHorizontal();

        namePNG = EditorGUILayout.TextField("Name Pose", namePNG);
        EditorGUILayout.BeginHorizontal();
        addVec = EditorGUILayout.Vector3Field("add Vec", addVec);
        if (GUILayout.Button(" + ", GUILayout.Width(50)) && namePNG != "" && namePNG != null) {
            myScript.SaveSkeleton(addVec, namePNG);
        }
        EditorGUILayout.EndHorizontal();

    }
}
