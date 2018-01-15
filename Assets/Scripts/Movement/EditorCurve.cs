using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorCurve : MonoBehaviour {

	public MovementTrail trail, trail2;

	private void Update() {
		if (Application.isEditor && !Application.isPlaying) 
		{
        	Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
       	 	foreach(GameObject obj in objects)
				{
					MovementTrail trail = obj.GetComponent<MovementTrail>();
					if(trail != null)
						trail.CalculateTrail();
				}
		}
	}
}
