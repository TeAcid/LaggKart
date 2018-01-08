using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMode : MonoBehaviour {
    private Light directionalLight;
    private Light[] lamps;
    private Light[] billboardSpotlights;
    private int numLights;
    private int numSpotlights;

	// Use this for initialization
	void Start () {
        directionalLight = transform.Find("DirectionalLight").GetComponent<Light>();
        lamps = GameObject.Find("Lamps").GetComponentsInChildren<Light>();
        numLights = lamps.Length;
        billboardSpotlights = GameObject.Find("Billboards").GetComponentsInChildren<Light>();
        numSpotlights = billboardSpotlights.Length;
        // Debug.Log("num lights " + numLights);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("l"))
        {
            directionalLight.enabled = !directionalLight.enabled;
            if(!directionalLight.enabled)
            {
                for(int i = 0; i < numLights; ++i)
                {
                    lamps[i].enabled = true;
                }

                for (int i = 0; i < numSpotlights; ++i)
                {
                    billboardSpotlights[i].enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < numLights; ++i)
                {
                    lamps[i].enabled = false;
                }

                for (int i = 0; i < numSpotlights; ++i)
                {
                    billboardSpotlights[i].enabled = false;
                }
            }
        }
	}
}
