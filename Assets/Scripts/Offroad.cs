using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offroad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Proga")
        {
            Debug.Log("Smo na progi");
        }
        else if (collision.gameObject.tag != "Proga")
        {
            Debug.Log("Nismo na progi");
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
