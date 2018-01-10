using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HindView : MonoBehaviour {
    private Camera mainCamera;
    private Camera altCamera;
    private RawImage hindViewCamera;
    private RawImage opponentViewCamera;
    //private AudioListener listener;
    
    private GameObject car;
    private Vector3 defaultOpponentCameraPos;
    // alt camera view 
    private int altViewVal = -1;

	// Use this for initialization
	void Start () {
        // cameras / views
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        altCamera = GameObject.FindGameObjectWithTag("AltCamera").GetComponent<Camera>();
        hindViewCamera = GameObject.Find("HindViewCamera").GetComponent<RawImage>();
        opponentViewCamera = GameObject.Find("OpponentViewCamera").GetComponent<RawImage>();
        //
        car = GameObject.Find("Car");
        defaultOpponentCameraPos = opponentViewCamera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // hind / opponent views
		if(Input.GetKey("b"))
        {
            hindViewCamera.enabled = true;
        }
        else if (Input.GetKey("n"))
        {
            Vector3 pos = defaultOpponentCameraPos;
            pos.y += 256;
            opponentViewCamera.transform.position = pos;
            opponentViewCamera.enabled = true;
        }
        else if (Input.GetKey("m"))
        {
            opponentViewCamera.enabled = true;
            hindViewCamera.enabled = true;
        }
        else
        {
            opponentViewCamera.transform.position = defaultOpponentCameraPos;
            opponentViewCamera.enabled = false;
            hindViewCamera.enabled = false;
        }

        // alt view
        if (Input.GetKeyDown("v"))
        {
            altViewVal = -altViewVal;
            if(altViewVal == 1)
            {
                mainCamera.enabled = false;
                altCamera.enabled = true;
            }
            else
            {
                altCamera.enabled = false;
                mainCamera.enabled = true;
            }
        }
	}
}
