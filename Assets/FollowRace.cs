using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FollowRace : MonoBehaviour {
    private GameObject car;
    WebCamTexture webCamTexture;

	// Use this for initialization
	void Start () {
        car = GameObject.Find("Car");
	}
	
	// Update is called once per frame
	void Update () {
        FollowObject(car);
	}

    private void FollowObject(GameObject o)
    {
        Vector3 pos = o.transform.position;
        pos.y += 30;
        pos.z += 30;
        transform.position = pos;

        // Camera facing car
        Camera cam = GameObject.FindGameObjectWithTag("CameraFollow").GetComponent<Camera>();
        cam.transform.LookAt(o.transform.position);
    }
}
