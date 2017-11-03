using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_collision : MonoBehaviour {
	
	public GameObject Explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "rocket")
        {
            Debug.Log("boom");
            var exp = (GameObject)Instantiate(Explosion, this.transform.position, this.transform.rotation);
            //GameObject.Destroy(this.gameObject);
            Destroy(other.gameObject);
            Destroy(exp, 1.0f);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
