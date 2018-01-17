using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public GameObject TestAura;
    public Material BoneMaterial;
    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private Test1 _BodyManager;

    public List<Vector3> Pose = new List<Vector3>();
    public List<Texture> PoseImage = new List<Texture>();

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>(){
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Update() {

        if (BodySourceManager == null) {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<Test1>();
        if (_BodyManager == null) {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null) {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data) {
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds) {
            if (!trackedIds.Contains(trackingId)) {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach (var body in data) {
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {
                if (!_Bodies.ContainsKey(body.TrackingId)) {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }

    private GameObject CreateBodyObject( ulong id ) {
        GameObject body = new GameObject("Body:" + id);
        DontDestroyOnLoad(body);
        body.transform.position = new Vector3(0, 12, -22);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++) {
            GameObject jointObj = GameObject.Instantiate(TestAura);
            if (jt == Kinect.JointType.HandRight || jt == Kinect.JointType.HandLeft) {
                jointObj.tag = "Hand";
                jointObj.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            }
            else {
                jointObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.0f, 0.00f);


            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        return body;
    }

    private void RefreshBodyObject( Kinect.Body body, GameObject bodyObject ) {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++) {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey(jt)) {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.position = GetVector3FromJoint(sourceJoint);

            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue) {
                lr.SetPosition(0, jointObj.position);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else {
                lr.enabled = false;
            }
        }
    }

    private static Color GetColorForState( Kinect.TrackingState state ) {
        switch (state) {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private static Vector3 GetVector3FromJoint( Kinect.Joint joint ) {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

    public void SaveSkeleton( Vector3 add, string name ) {
        if (name != "" && name != null) {
            ScreenCapture.CaptureScreenshot("Assets/Bas/Resources/ScreenShots/" + name + ".jpg");
            //Sprite addToList = Resources.Load(name) as Sprite;
            //PoseImage.Add(addToList);
            StartCoroutine(Delay(name));
            Pose.Add(add);
        }
    }

    IEnumerator Delay( string name ) {
        yield return new WaitForSeconds(5);
        Texture addToList = Resources.Load(name) as Texture;
        print(addToList);
        PoseImage.Add(addToList);
    }

    public void DeleteSkeleton( int del ) {
        //Texture
        //Resources.UnloadAsset(delImage);
        Pose.RemoveAt(del);
        PoseImage.RemoveAt(del);

    }

}
